using StackExchange.Redis;

namespace WebApiWithRedisCache.Services
{
    //使用Redis中的BitMap数据类型来实现签到
    public class SignInService
    {
        private readonly IDatabase _redis;
        private const string SignKeyPrefix = "user:sign:";

        public SignInService(IConnectionMultiplexer redis)
        {
            _redis = redis.GetDatabase();
        }

        // 用户签到
        public async Task<bool> SignInAsync(string userId, DateTime date)
        {
            // 计算一年中的第几天作为偏移量（0-based）
            var offset = GetDayOfYearOffset(date);
            var key = GetUserSignKey(userId, date.Year);

            // 检查是否已签到
            // 当 Key 不存在时：返回 false（即 bit 值为 0）
            bool alreadySigned = await _redis.StringGetBitAsync(key, offset);
            if (alreadySigned) return false;

            // 执行签到
            //true表示将该Bit设置为1
            await _redis.StringSetBitAsync(key, offset, true);

            // 设置键的过期时间（1年后自动删除——明年今日）
            await _redis.KeyExpireAsync(key, GetExpirationToNextSameDay());

            return true;
        }

        // 检查某天是否签到
        public async Task<bool> CheckSignedAsync(string userId, DateTime date)
        {
            var offset = GetDayOfYearOffset(date);
            var key = GetUserSignKey(userId, date.Year);
            return await _redis.StringGetBitAsync(key, offset);
        }

        // 获取当月签到情况
        public async Task<Dictionary<int, bool>> GetMonthSignStatusAsync(string userId, int year, int month)
        {
            var result = new Dictionary<int, bool>();
            var daysInMonth = DateTime.DaysInMonth(year, month);
            var key = GetUserSignKey(userId, year);

            for (int day = 1; day <= daysInMonth; day++)
            {
                var date = new DateTime(year, month, day);
                var offset = GetDayOfYearOffset(date);
                result[day] = await _redis.StringGetBitAsync(key, offset);
            }

            return result;
        }

        // 统计连续签到天数
        public async Task<int> GetContinuousSignCountAsync(string userId, DateTime endDate)
        {
            var key = GetUserSignKey(userId, endDate.Year);
            int count = 0;

            // 获取当年的天数（考虑闰年）
            int daysInYear = DateTime.IsLeapYear(endDate.Year) ? 366 : 365;

            for (int i = 0; i <= daysInYear; i++) // 最多检查当年所有天数
            {
                var date = endDate.AddDays(-i);

                // 如果日期已经跨年，则停止检查
                if (date.Year != endDate.Year) break;

                var offset = GetDayOfYearOffset(date);
                if (!await _redis.StringGetBitAsync(key, offset))
                {
                    break;
                }
                count++;
            }

            return count;
        }

        private static string GetUserSignKey(string userId, int year)
        {
            return $"{SignKeyPrefix}{userId}:{year}";
        }

        private static int GetDayOfYearOffset(DateTime date)
        {
            return date.DayOfYear - 1; // 偏移量从0开始
        }
        public static TimeSpan GetExpirationToNextSameDay()
        {
            var today = DateTime.Today;
            var nextYearSameDay = today.AddYears(1);
            return nextYearSameDay - today;
        }
    }
}
