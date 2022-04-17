using Voting.BAL.Models;
using Voting.DAL.Entities;

namespace Voting.BAL.Contracts
{
    public interface IAccountService
    {
        public Task<IEnumerable<Account>> GetAll();
        public Task<GenericResult<Account>> RegisterAsync(RegisterDto model);
        public Task<GenericResult<Account>> LoginAsync(LoginDto model);

    }
}
