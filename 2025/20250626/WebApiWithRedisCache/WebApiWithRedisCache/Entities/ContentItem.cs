namespace WebApiWithRedisCache.Entities
{
    public class ContentItem
    {
        public string AuthorId { get; set; }
        public string AuthorName { get; set; }
        public string Text { get; set; }
        public long Timestamp { get; set; }
        public DateTime PublishTime => new DateTime(Timestamp);
    }
}
