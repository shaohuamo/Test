namespace WebApiWithRedisCache.Entities
{
    public class ScrollResponse<T>
    {
        /// <summary>
        /// 当前页的数据项集合
        /// </summary>
        public IEnumerable<T> Data { get; set; } = Enumerable.Empty<T>();

        /// <summary>
        /// 下次请求使用的游标
        /// </summary>
        public long? NextCursor { get; set; }

        /// <summary>
        /// 是否还有更多数据
        /// </summary>
        public bool HasMore { get; set; }
    }
}
