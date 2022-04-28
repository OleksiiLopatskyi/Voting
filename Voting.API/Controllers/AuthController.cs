using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Voting.BAL;
using Voting.BAL.Attributes;
using Voting.BAL.Contracts;
using Voting.BAL.Models;
using Voting.DAL.Entities;

namespace Voting.API.Controllers
{
    [Route("api/auth")]
    public class AuthController : CustomController
    {
        private readonly IAuthService _accountService;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IAuthService accountService, ILogger<AuthController> logger)
        {
            _accountService = accountService;
            _logger = logger;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody]LoginDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _accountService.LoginAsync(model);
            return CustomResult(result);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody]RegisterDto model)
        {
            _logger.LogInformation("Register executing...");
            var result = await _accountService.RegisterAsync(model);
            return CustomResult(result);
        }
    }
}
