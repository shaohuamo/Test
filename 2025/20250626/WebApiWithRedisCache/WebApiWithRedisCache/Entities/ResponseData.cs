namespace WebApiWithRedisCache.Entities
{
    public class ResponseData
    {
        public string? DistributedKey { get; set; }
        public string? DistributedValue { get; set; }
        public string? ConnectionMultiplexerKey { get; set; }
        public string? ConnectionMultiplexerValue { get; set; }
    }
}
