using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiWithRedisCache.Services;

namespace WebApiWithRedisCache.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FollowController : ControllerBase
    {
        private readonly FollowService _followService;

        public FollowController(FollowService followService)
        {
            _followService = followService;
        }

        [HttpPost("{followerId}/follow/{followeeId}")]
        public async Task<IActionResult> Follow(string followerId, string followeeId)
        {
            if (await _followService.IsFollowingAsync(followerId, followeeId))
            {
                return BadRequest("Already following");
            }

            var success = await _followService.FollowAsync(followerId, followeeId);
            return success ? Ok() : StatusCode(500);
        }

        [HttpDelete("{followerId}/unfollow/{followeeId}")]
        public async Task<IActionResult> Unfollow(string followerId, string followeeId)
        {
            if (!await _followService.IsFollowingAsync(followerId, followeeId))
            {
                return BadRequest("Not following");
            }

            var success = await _followService.UnfollowAsync(followerId, followeeId);
            return success ? Ok() : StatusCode(500);
        }

        [HttpGet("{userId}/followers")]
        public async Task<IActionResult> GetFollowers(string userId, [FromQuery] int page = 1, [FromQuery] int pageSize = 20)
        {
            var followers = await _followService.GetFollowersAsync(userId, page);
            var count = await _followService.GetFollowersCountAsync(userId);

            return Ok(new
            {
                Data = followers,
                TotalCount = count,
                Page = page,
                PageSize = pageSize
            });
        }

        [HttpGet("common - following")]
        public async Task<IEnumerable<string>> GetCommonFollowing(string userId1,string userId2)
        {
             return await _followService.GetCommonFollowingAsync(userId1, userId2);
        }
    }
}
