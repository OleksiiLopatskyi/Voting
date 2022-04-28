using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Voting.BAL.Attributes;
using Voting.BAL.Contracts;
using Voting.DAL.Entities;

namespace Voting.API.Controllers
{
    [Route("api/admin")]
    [ApiController]
    [MyAuthorize(Role = UserRoles.Admin)]
    public class AdminController : CustomController
    {
        private readonly UserManager<User> _userManager;
        private readonly IAuthService _authService;
        private readonly IUserService _userService;
        private readonly IAdminService _adminService;

        public AdminController(UserManager<User> userManager,
            IAuthService authService,
            IUserService userService,
            IAdminService adminService)
        {
            _userManager = userManager;
            _authService = authService;
            _userService = userService;
            _adminService = adminService;
        }

        [HttpGet("users")]
        public async Task<IActionResult> GetUsers()
        {
            var result = await _userService.GetAllAsync(GetUserId());
            return Ok(result);
        }
        
        [HttpPut("switch-status/{userId}")]
        public async Task<IActionResult> SwitchStatus(string userId)
        {
            var result = await _adminService.SwitchStatusAsync(userId);
            return CustomResult(result);
        }

    }
}
