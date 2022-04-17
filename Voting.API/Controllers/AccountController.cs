using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
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
        public IAccountService _accountService;
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }
        [HttpGet]
        public async Task<IActionResult> getUser()
        {
            return Ok(await _accountService.GetAccount(GetAccountId()));
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody]LoginDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _accountService.LoginAsync(model);
            await Authenticate(result.Data);
            return CustomResult(result);
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody]RegisterDto model)
        {
            var result = await _accountService.RegisterAsync(model);
            return CustomResult(result);
        }
        private async Task Authenticate(Account model)
        {
            var claims = new List<Claim>()
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, model.Username),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, model.Role.Name),
                    new Claim("Id",model.Id.ToString()),
                };
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie",
                                                   ClaimsIdentity.DefaultNameClaimType,
                                                   ClaimsIdentity.DefaultRoleClaimType
                                                   );
            await HttpContext
                .SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(id));
        }
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Ok();
        }
    }
}
