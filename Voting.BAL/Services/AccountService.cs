using System;
using System.Collections.Generic;
using System.Linq;
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
        public AccountService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Account>> GetAll()
        {
            return await _unitOfWork.AccountRepository.FindAllAsync();
        }
        public async Task<GenericResult<Account>> GetAccount(int id)
        {
            return new GenericResult<Account>() { Data = await _unitOfWork.AccountRepository
                .FindEntityAsync(a=>a.Id==id) };
        }

        public async Task<GenericResult<Account>> LoginAsync(LoginDto model)
        {
            try
            {
                var foundUser = await _unitOfWork.AccountRepository
                    .FindEntityAsync(a => a.Email == model.Email && a.Password == model.Password);
                if (foundUser == null)
                {
                    return new GenericResult<Account>
                    {
                        ErrorMessage = "Incorrect Email or Password",
                        StatusCode = StatusCode.BadRequest
                    };
                }
                return new GenericResult<Account> { Data = foundUser };
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
                if(foundRole == null)
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
