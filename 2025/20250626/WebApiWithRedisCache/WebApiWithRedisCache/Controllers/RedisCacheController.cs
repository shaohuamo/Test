using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using StackExchange.Redis;
using WebApiWithRedisCache.Entities;

namespace WebApiWithRedisCache.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RedisCacheController : ControllerBase
    {
        private readonly IDatabase _db;
        private readonly IDistributedCache _cache;
        public RedisCacheController(IDistributedCache cache, IConnectionMultiplexer redis)
        {
            _cache = cache;
            _db = redis.GetDatabase();
        }

        [HttpGet]
        [Route("get/{key}")]
        public async Task<ResponseData> GetCache(string key)
        {
            var distributedKey = "IDistributed:" + key;
            var connectionMultiplexerKey = "IConnectionMultiplexer:" + key;
            string? cache1 = await _cache.GetStringAsync(distributedKey);
            string? cache2 = await _db.StringGetAsync(connectionMultiplexerKey);

            var cacheData = new ResponseData()
            {
                DistributedKey = distributedKey,
                ConnectionMultiplexerKey = connectionMultiplexerKey,
                DistributedValue = cache1,
                ConnectionMultiplexerValue = cache2
            };

            return cacheData;
        }

        [HttpPost]
        [Route("set")]
        public async Task<string> SetCache(CacheData cacheData)
        {
            var distributedKey = "IDistributed:" + cacheData.Key;
            var connectionMultiplexerKey = "IConnectionMultiplexer:" + cacheData.Key;

            await _cache.SetStringAsync(distributedKey, cacheData.Value!);
            await _db.StringSetAsync(connectionMultiplexerKey, cacheData.Value!);

            //await _cache.SetStringAsync(key, value, new DistributedCacheEntryOptions
            //{
            //    // 1小时后强制过期
            //    AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1),
            //    // 10分钟内无访问则过期
            //    SlidingExpiration = TimeSpan.FromMinutes(10)            
            //});

            return "cache is set successfully";
        }
    }
}
