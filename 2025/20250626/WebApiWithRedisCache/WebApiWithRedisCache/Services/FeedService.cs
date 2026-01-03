using StackExchange.Redis;
using WebApiWithRedisCache.Entities;

namespace WebApiWithRedisCache.Services
{
    public class FeedService
    {
        private readonly IDatabase _redis;
        private const string InboxKeyPrefix = "inbox:";
        private const string ContentKeyPrefix = "content:";
        private const string FollowersKeyPrefix = "followers:";
        private const string ContentByTimeKey = "contentByTime";

        public FeedService(IConnectionMultiplexer redisConnection)
        {
            _redis = redisConnection.GetDatabase();
        }

        // ① 发布内容并推送到粉丝收件箱
        public async Task<string> PublishContentAsync(string authorId, string contentText)
        {
            // 生成内容ID (可以使用雪花算法等分布式ID生成器)
            var contentId = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString();
            var timestamp = DateTime.UtcNow.Ticks;

            // 存储内容
            await _redis.HashSetAsync($"{ContentKeyPrefix}{contentId}", new HashEntry[] {
            new("authorId", authorId),
            new("text", contentText),
            new("timestamp", timestamp),
            // 可以添加更多字段如 contentType, mediaUrl 等
        });

            // 添加到全局时间排序集合(仅用于按时间查询)
            await _redis.SortedSetAddAsync(ContentByTimeKey, contentId, timestamp);

            // 获取所有粉丝(生产环境应考虑分页处理大量粉丝)
            var followers = await _redis.SetMembersAsync($"{FollowersKeyPrefix}{authorId}");

            // 使用批处理推送到粉丝收件箱
            var batch = _redis.CreateBatch();
            var pushTasks = followers
                .Select(follower => batch.SetAddAsync($"{InboxKeyPrefix}{follower}", contentId))
                .ToList();

            batch.Execute();
            await Task.WhenAll(pushTasks);

            return contentId;
        }

        /// <summary>
        /// 粉丝获取收件箱内容(滚动分页)
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="cursor">游标(上次获取的最后内容时间戳)</param>
        /// <param name="limit">每页数量</param>
        /// <returns>(内容列表, 下次游标)</returns>
        public async Task<ScrollResponse<ContentItem>> GetInboxContentsScrollAsync(
            string userId,
            long? cursor = null,
            int limit = 5)
        {
            // 获取收件箱中的所有内容ID(无序)
            var inboxContentIds = await _redis.SetMembersAsync($"{InboxKeyPrefix}{userId}");

            if (inboxContentIds.Length == 0)
                return new ScrollResponse<ContentItem>();

            // 从全局时间排序集合中筛选出用户收件箱中的内容，并按时间排序
            var sortedContentIds = (await _redis.SortedSetRangeByScoreAsync(
                ContentByTimeKey,
                take: inboxContentIds.Length,
                order: Order.Descending))
                .Where(id => inboxContentIds.Contains(id))
                .ToList();

            // 应用游标筛选
            var filteredContentIds = cursor.HasValue
                ? sortedContentIds
                    .SkipWhile(id => GetTimestampFromId(id) >= cursor.Value)
                    .Take(limit)
                    .ToList()
                : sortedContentIds
                    .Take(limit)
                    .ToList();

            if (filteredContentIds.Count == 0)
                return new ScrollResponse<ContentItem>();

            // 批量获取内容详情
            var batch = _redis.CreateBatch();
            var contentTasks = filteredContentIds
                .Select(id => batch.HashGetAllAsync($"{ContentKeyPrefix}{id}"))
                .ToList();

            batch.Execute();
            var contents = await Task.WhenAll(contentTasks);

            // 转换为内容对象
            var items = contents
                .Where(c => c.Length > 0)
                .Select(content => new ContentItem
                {
                    Text = content.First(x => x.Name == "text").Value.ToString(),
                    AuthorId = content.First(x => x.Name == "authorId").Value.ToString(),
                    Timestamp = long.Parse(content.First(x => x.Name == "timestamp").Value)
                })
                .ToList();

            // 计算下次游标(最后一项的时间戳)
            var nextCursor = items.LastOrDefault()?.Timestamp;

            return new ScrollResponse<ContentItem>
            {
                Data = items,
                NextCursor = nextCursor,
                HasMore = nextCursor.HasValue && sortedContentIds.Last().ToString() != filteredContentIds.Last().ToString()
            };
        }

        // 获取收件箱内容总数
        public async Task<long> GetInboxCountAsync(string userId)
        {
            return await _redis.SortedSetLengthAsync($"{InboxKeyPrefix}{userId}");
        }

        private long GetTimestampFromId(RedisValue id)
        {
            // 如果ID不包含时间戳，从内容中获取
            var content = _redis.HashGet($"{ContentKeyPrefix}{id}", "timestamp");
            return (long)content;
        }
    }

}
