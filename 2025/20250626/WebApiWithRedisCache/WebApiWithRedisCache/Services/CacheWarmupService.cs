using Microsoft.Extensions.Caching.Distributed;
using StackExchange.Redis;

namespace WebApiWithRedisCache.Services
{
    //使用Redis缓存预热
    public class CacheWarmupService
    {
        private readonly IDatabase _redis;
        private const string _hotDataId = "test";

        public CacheWarmupService(IConnectionMultiplexer redisConnection)
        {
            _redis = redisConnection.GetDatabase();
        }

        public async Task WarmUpAsync()
        {
            // 预热热门商品数据
            var cacheKey = $"hotData:{_hotDataId}";
            await _redis.StringSetAsync(cacheKey,"hotDataForTest" ,TimeSpan.FromHours(1));
        }
    }
}
