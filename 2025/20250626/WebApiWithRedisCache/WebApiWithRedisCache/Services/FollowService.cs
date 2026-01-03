using StackExchange.Redis;

namespace WebApiWithRedisCache.Services
{
    public class FollowService
    {
        private readonly IDatabase _redis;
        private const string FollowersKeyPrefix = "followers:";
        private const string FollowingKeyPrefix = "following:";
        private const string UserKeyPrefix = "user:";

        public FollowService(IConnectionMultiplexer redis)
        {
            _redis = redis.GetDatabase();
        }

        // 关注用户
        public async Task<bool> FollowAsync(string followerId, string followeeId)
        {
            if (followerId == followeeId) return false;
            if (await IsFollowingAsync(followerId, followeeId)) return false;

            // 使用事务保证原子性
            var transaction = _redis.CreateTransaction();

            // 添加到关注集合
            transaction.SetAddAsync($"{FollowingKeyPrefix}{followerId}", followeeId);
            // 添加到粉丝集合
            transaction.SetAddAsync($"{FollowersKeyPrefix}{followeeId}", followerId);

            if (!await transaction.ExecuteAsync())
                return false; // 事务失败

            // 更新计数器 - 使用批处理提高性能
            var batch = _redis.CreateBatch();
            var task1 = batch.HashIncrementAsync($"user:{followeeId}", "followersCount");
            var task2 = batch.HashIncrementAsync($"user:{followerId}", "followingCount");
            batch.Execute();
            await Task.WhenAll(task1, task2);

            return true;
        }

        // 取消关注
        public async Task<bool> UnfollowAsync(string followerId, string followeeId)
        {
            if (!await IsFollowingAsync(followerId, followeeId)) return false;

            // 使用事务保证原子性
            var transaction = _redis.CreateTransaction();

            // 从关注集合移除
            transaction.SetRemoveAsync($"{FollowingKeyPrefix}{followerId}", followeeId.ToString());

            // 从粉丝集合移除
            transaction.SetRemoveAsync($"{FollowersKeyPrefix}{followeeId}", followerId.ToString());

            if (!await transaction.ExecuteAsync())
                return false; // 事务失败

            // 更新计数器 - 使用批处理提高性能
            var batch = _redis.CreateBatch();
            var task1 = batch.HashDecrementAsync($"user:{followeeId}", "followersCount");
            var task2 = batch.HashDecrementAsync($"user:{followerId}", "followingCount");
            batch.Execute();
            await Task.WhenAll(task1, task2);

            return true;
        }

        // 检查是否关注
        public async Task<bool> IsFollowingAsync(string followerId, string followeeId)
        {
            return await _redis.SetContainsAsync($"{FollowingKeyPrefix}{followerId}", followeeId.ToString());
        }

        // 获取粉丝列表
        public async Task<IEnumerable<string>> GetFollowersAsync(string userId, int limit = 20)
        {
            var followers = await _redis.SetMembersAsync($"{FollowersKeyPrefix}{userId}");
            return followers.Take(limit).Select(x => x.ToString());
        }

        // 获取关注列表
        public async Task<IEnumerable<string>> GetFollowingAsync(string userId, int limit = 20)
        {
            var following = await _redis.SetMembersAsync($"{FollowingKeyPrefix}{userId}");
            return following.Take(limit).Select(x => x.ToString());
        }

        // 获取共同关注
        public async Task<IEnumerable<string>> GetCommonFollowingAsync(string userId1, string userId2)
        {
            var result = await _redis.SetCombineAsync(SetOperation.Intersect,
                $"{FollowingKeyPrefix}{userId1}",
                $"{FollowingKeyPrefix}{userId2}");

            return result.Select(x => x.ToString());
        }

        // 获取粉丝数
        public async Task<int> GetFollowersCountAsync(string userId)
        {
            var count = await _redis.HashGetAsync($"{UserKeyPrefix}{userId}", "followersCount");
            return count.HasValue ? (int)count : 0;
        }

        // 获取关注数
        public async Task<int> GetFollowingCountAsync(string userId)
        {
            var count = await _redis.HashGetAsync($"{UserKeyPrefix}{userId}", "followingCount");
            return count.HasValue ? (int)count : 0;
        }
    }
}
