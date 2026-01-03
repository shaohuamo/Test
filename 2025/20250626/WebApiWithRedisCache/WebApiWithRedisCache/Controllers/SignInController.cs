using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiWithRedisCache.Services;

namespace WebApiWithRedisCache.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SignInController : ControllerBase
    {
        private readonly SignInService _signService;

        public SignInController(SignInService signService)
        {
            _signService = signService;
        }

        [HttpPost("{userId}")]
        public async Task<IActionResult> SignIn(string userId)
        {
            var signed = await _signService.SignInAsync(userId, DateTime.Today);
            return Ok(new { success = signed });
        }

        [HttpGet("{userId}/status")]
        public async Task<IActionResult> GetStatus(string userId, [FromQuery] int? year, [FromQuery] int? month)
        {
            year ??= DateTime.Now.Year;
            month ??= DateTime.Now.Month;

            var status = await _signService.GetMonthSignStatusAsync(userId, year.Value, month.Value);
            return Ok(status);
        }

        [HttpGet("{userId}/continuous")]
        public async Task<IActionResult> GetContinuousDays(string userId)
        {
            var days = await _signService.GetContinuousSignCountAsync(userId, DateTime.Today);
            return Ok(new { continuousDays = days });
        }
    }
}
