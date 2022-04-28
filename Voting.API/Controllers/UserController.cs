using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Voting.BAL.Attributes;
using Voting.BAL.Contracts;
using Voting.BAL.Models;
using Voting.DAL.Entities;

namespace Voting.API.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : CustomController
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
            _userService = userService;
        }

        [MyAuthorize(Role = UserRoles.User)]
        [HttpGet("{userId}")]
        public async Task<IActionResult> Profile(string userId)
        {
            if (userId == GetUserId())
            {
                return Ok(new
                {
                    data = new
                    {
                        id = userId
                    }
                }
                );
            }
            var result = await _userService.GetProfileAsync(userId);
            return Ok(result);
        }

        [MyAuthorize(Role = UserRoles.User)]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _userService.GetAllAsync(GetUserId());
            return CustomResult(result);
        }

        [MyAuthorize(Role = UserRoles.User)]
        [HttpGet("profile")]
        public async Task<IActionResult> Profile()
        {
            var result = await _userService.GetUserAsync(GetUserId());
            return CustomResult(result);
        }

        [MyAuthorize(Role = UserRoles.User)]
        [HttpPut]
        public async Task<IActionResult> Update([FromForm] UserDto userDto)
        {
            var result = await _userService.UpdateAsync(GetUserId(), userDto);
            return CustomResult(result);
        }

        [MyAuthorize(Role = UserRoles.User)]
        [HttpGet("voting-stats")]
        public async Task<IActionResult> VotingStats()
        {
            var result = await _userService.GetVotingStatsAsync(GetUserId());
            return CustomResult(result);
        }

        [MyAuthorize(Role = UserRoles.Admin)]
        [HttpDelete("{userId}")]
        public async Task<IActionResult> Delete(string userId)
        {
            var result = await _userService.DeleteAsync(userId);
            return CustomResult(result);
        }
    }
}
