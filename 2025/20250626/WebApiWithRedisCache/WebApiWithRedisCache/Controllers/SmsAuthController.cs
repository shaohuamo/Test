using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiWithRedisCache.Entities;
using WebApiWithRedisCache.Services;

namespace WebApiWithRedisCache.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SmsAuthController : ControllerBase
    {
        private readonly SmsCodeService _smsCodeService;

        public SmsAuthController(SmsCodeService smsCodeService)
        {
            _smsCodeService = smsCodeService;
        }

        //http://localhost:5011/api/SmsAuth/send-code?phoneNumber=15605169336
        // 发送验证码
        [HttpPost("send-code")]
        public async Task<IActionResult> SendCode(string phoneNumber)
        {
            var code = await _smsCodeService.SendCodeAsync(phoneNumber);

            return Ok($"验证码是：{code}");
        }

        // 验证登录
        [HttpPost("verify-code")]
        public async Task<IActionResult> VerifyCode(VerifyCodeRequest verifyCode)
        {
            var (success, message) = await _smsCodeService.VerifyCodeAsync(
                verifyCode.PhoneNumber!,
                verifyCode.Code!
            );

            return success ? Ok(new { Token = message }) : BadRequest(message);
        }
    }
}
