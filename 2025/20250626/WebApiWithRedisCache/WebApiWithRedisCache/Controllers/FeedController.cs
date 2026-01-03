using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiWithRedisCache.Entities;
using WebApiWithRedisCache.Services;

namespace WebApiWithRedisCache.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedController : ControllerBase
    {
        private readonly FeedService _feedService;
        private readonly ILogger<FeedController> _logger;

        public FeedController(FeedService feedService, ILogger<FeedController> logger)
        {
            _feedService = feedService;
            _logger = logger;
        }

        /// <summary>
        /// 发布内容
        /// </summary>
        [HttpPost("publish")]
        public async Task<IActionResult> PublishContent(PublishRequest request)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(request.ContentText))
                    return BadRequest("内容不能为空");

                var contentId = await _feedService.PublishContentAsync(
                    request.AuthorId,
                    request.ContentText);

                return Ok(new { ContentId = contentId });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"发布内容失败，作者: {request.AuthorId}");
                return StatusCode(500, "发布内容失败");
            }
        }

        /// <summary>
        /// 获取用户收件箱内容(滚动分页)
        /// </summary>
        [HttpGet("inbox/scroll")]
        public async Task<IActionResult> GetInboxScroll(
            [FromQuery] string? userId,
            [FromQuery] long? cursor = null,
            [FromQuery] int limit = 5)
        {
            try
            {
                if (limit < 1 || limit > 100) return BadRequest("每页数量必须在1-100之间");

                var response = await _feedService.GetInboxContentsScrollAsync(
                    userId, cursor, limit);

                return Ok(response);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"获取收件箱(滚动)失败，用户: {userId}");
                return StatusCode(500, "获取收件箱失败");
            }
        }
    }
}
