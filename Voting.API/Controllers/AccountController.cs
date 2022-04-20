using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Voting.BAL;
using Voting.BAL.Contracts;
using Voting.BAL.Models;
using Voting.DAL.Entities;

namespace Voting.API.Controllers
{
    [Route("api/account")]
    public class AccountController : CustomController
    {
        private readonly IAccountService _accountService;
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetUser()
        {
            var result = await _accountService.GetAccount(GetAccountId());
            return CustomResult(result);
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
            var result = await _accountService.RegisterAsync(model);
            return CustomResult(result);
        }
    }
}
