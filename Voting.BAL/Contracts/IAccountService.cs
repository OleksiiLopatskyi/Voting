using Voting.BAL.Models;
using Voting.DAL.Entities;

namespace Voting.BAL.Contracts
{
    public interface IAccountService
    {
        public Task<GenericResult<Account>> GetAccount(int id);
        public Task<IEnumerable<Account>> GetAll();
        public Task<GenericResult<Account>> RegisterAsync(RegisterDto model);
        public Task<GenericResult<string>> LoginAsync(LoginDto model);
    }
}
