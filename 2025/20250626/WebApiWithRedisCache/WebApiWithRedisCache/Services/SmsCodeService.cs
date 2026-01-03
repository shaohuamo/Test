using StackExchange.Redis;

namespace WebApiWithRedisCache.Services
{
    //使用Redis来实现短信登录
    public class SmsCodeService
    {
        private readonly IDatabase _redis;
        private readonly Random _random = new();

        //使用IConnectionMultiplexer而不是用IDistributedCache的原因是
        //IDistributedCache不支持原子性操作
        public SmsCodeService(IConnectionMultiplexer redis)
        {
            _redis = redis.GetDatabase();
        }

        // 发送短信验证码
        public async Task<string> SendCodeAsync(string phoneNumber)
        {
            // 生成 6 位随机验证码
            string code = _random.Next(100000, 999999).ToString();

            // 存储到 Redis，5 分钟过期
            await _redis.StringSetAsync(
                $"sms:login:{phoneNumber}",
                code,
                TimeSpan.FromMinutes(5)
            );

            // 模拟发送短信（实际应调用短信服务商 API）
            Console.WriteLine($"发送验证码至 {phoneNumber}: {code}");
            return code;
        }

        // 验证短信登录
        public async Task<(bool Success, string Message)> VerifyCodeAsync(string phoneNumber, string inputCode)
        {
            // 检查尝试次数（防刷）
            string retryKey = $"sms:login:retry:{phoneNumber}";

            //StringIncrementAsync方法是原子性操作
            long retryCount = await _redis.StringIncrementAsync(retryKey);

            await _redis.KeyExpireAsync(retryKey, TimeSpan.FromMinutes(5)); // 5 分钟过期

            if (retryCount > 3)
            {
                return (false, "尝试次数过多，请稍后再试");
            }

            // 获取存储的验证码
            string? storedCode = await _redis.StringGetAsync($"sms:login:{phoneNumber}");

            if (string.IsNullOrEmpty(storedCode))
            {
                return (false, "验证码已过期或未发送");
            }

            if (inputCode != storedCode)
            {
                return (false, "验证码错误");
            }

            // 验证成功，删除 Redis 中的验证码
            await _redis.KeyDeleteAsync($"sms:login:{phoneNumber}");
            await _redis.KeyDeleteAsync(retryKey); // 清除尝试次数

            // 生成 Token（示例，实际可用 JWT）
            string token = GenerateJwtToken(phoneNumber);

            return (true, token);
        }

        private string GenerateJwtToken(string phoneNumber)
        {
            // 实际应使用 JWT 库（如 Microsoft.IdentityModel.Tokens）
            return $"JWT-TOKEN-FOR-{phoneNumber}";
        }
    }
}
