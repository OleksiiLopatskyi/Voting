using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Voting.BAL.Contracts;
using Voting.BAL.Models;
using Voting.DAL.Contracts;
using Voting.DAL.Entities;

namespace Voting.BAL.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;
        private readonly IPairService _pairService;
        private readonly UserManager<User> _userManager;
        public AuthService(UserManager<User> userManager,
            IConfiguration configuration,
            IUnitOfWork unitOfWork,
            IPairService pairService)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _configuration = configuration;
            _pairService = pairService;
        }

        public async Task<LoginResult<string>> LoginAsync(LoginDto model)
        {
            try
            {
                var account = await _userManager.FindByEmailAsync(model.Email);
                if (account == null)
                {
                    return new LoginResult<string>
                    {
                        Message = "Неправильно введені дані.",
                        StatusCode = StatusCode.BadRequest
                    };
                }
                var password = await _userManager.CheckPasswordAsync(account, model.Password);
                if (!password)
                {
                    return new LoginResult<string>
                    {
                        Message = "Неправильно введені дані.",
                        StatusCode = StatusCode.BadRequest
                    };
                }
                if (!account.Active)
                {
                    return new LoginResult<string>
                    {
                        Message = "Ваш аккаунт не підтверджений адміном",
                        StatusCode = StatusCode.BadRequest
                    };
                }
                return new LoginResult<string>
                {
                    Data = GenerateToken(account),
                    Role = account.Role.ToString()
                };
            }
            catch (Exception ex)
            {
                return new LoginResult<string>
                {
                    Message = "Виникла проблема. Спробуйте, будь ласка, пізніше",
                    StatusCode = StatusCode.InternalServerError
                };
            }

        }

        private string GenerateToken(User account)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.UTF8.GetBytes(_configuration["JWT:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Name, account.UserName),
                        new Claim(ClaimTypes.Email, account.Email),
                        new Claim("Id", account.Id),
                        new Claim(ClaimsIdentity.DefaultRoleClaimType,
                        account.Role.ToString())
                    }),
                Expires = DateTime.UtcNow.AddMinutes(10),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public async Task<Result> RegisterAsync(RegisterDto model)
        {
            try
            {
                var pairs = await _pairService.GeneratePairsAsync();
                var account = new User()
                {
                    Email = model.Email,
                    UserName = model.Username,
                    Role = UserRoles.User,
                    FavoriteModel = null,
                    Pairs = pairs.ToList()
                };
                account.Pairs.ToList().ForEach(pair => pair.UserId = account.Id);
                var checkUser = await _userManager.FindByEmailAsync(account.Email);
                if (checkUser != null)
                {
                    return new Result<User>
                    {
                        Message = "Користувач з такою ел.поштою вже існує.",
                        StatusCode = StatusCode.BadRequest
                    };
                }
                var result = await _userManager.CreateAsync(account, model.Password);
                return new Result
                {
                    Message = "Користувача успішно створено.",
                    StatusCode = StatusCode.Created
                };
            }
            catch (Exception ex)
            {
                return new Result
                {
                    // Message = "Виникла проблема. Спробуйте, будь ласка, пізніше",
                    Message = ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
    }
}
