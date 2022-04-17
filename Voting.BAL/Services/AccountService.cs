using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Voting.BAL.Builders;
using Voting.BAL.Contracts;
using Voting.BAL.Models;
using Voting.DAL.Contracts;
using Voting.DAL.Entities;

namespace Voting.BAL.Services
{
    public class AccountService : IAccountService
    {
        private readonly IUnitOfWork _unitOfWork;
        private IConfiguration _configuration;
        public AccountService(IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
        }

        public async Task<IEnumerable<Account>> GetAll()
        {
            return await _unitOfWork.AccountRepository.FindAllAsync();
        }
        public async Task<GenericResult<Account>> GetAccount(int id)
        {
            return new GenericResult<Account>()
            {
                Data = await _unitOfWork.AccountRepository
                .FindEntityAsync(a => a.Id == id)
            };
        }

        public async Task<GenericResult<string>> LoginAsync(LoginDto model)
        {
            try
            {
                var foundUser = await _unitOfWork.AccountRepository
                    .FindEntityAsync(a => a.Email == model.Email && a.Password == model.Password);
                if (foundUser == null)
                {
                    return new GenericResult<string>
                    {
                        ErrorMessage = "Incorrect Email or Password",
                        StatusCode = StatusCode.BadRequest
                    };
                }

                var token = GenerateToken(foundUser);
                return new GenericResult<string> { Data = token};
            }
            catch (Exception ex)
            {
                return new GenericResult<string>
                {
                    ErrorMessage = ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
        private string GenerateToken(Account account)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.UTF8.GetBytes(_configuration["JWT:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Name, account.Username),
                        new Claim(ClaimTypes.Email, account.Email),
                        new Claim(ClaimTypes.Role, account.Role.Name),
                        new Claim("Id", account.Id.ToString())
                    }),
                Expires = DateTime.UtcNow.AddMinutes(10),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        public async Task<GenericResult<Account>> RegisterAsync(RegisterDto model)
        {
            try
            {
                var foundUser = await _unitOfWork.AccountRepository
                    .FindEntityAsync(a => a.Email == model.Email);
                var foundRole = await _unitOfWork.RoleRepository
                    .FindEntityAsync(r => r.Name == AccountConstants.UserRole);
                if (foundUser != null)
                {
                    return new GenericResult<Account> { ErrorMessage = "User with that is already exists" };
                }
                if (foundRole == null)
                {
                    foundRole = new Role { Name = AccountConstants.UserRole };
                }

                var createdAccount = new AccountBuilder()
                    .WithEmail(model.Email)
                    .WithUsername(model.Username)
                    .WithRole(foundRole)
                    .WithPassword(model.Password)
                    .Build();

                await _unitOfWork.AccountRepository.CreateAsync(createdAccount);
                await _unitOfWork.SaveAsync();
                return new GenericResult<Account> { StatusCode = StatusCode.Success };
            }
            catch (Exception ex)
            {
                return new GenericResult<Account>
                {
                    ErrorMessage = ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }

        }
    }
}
