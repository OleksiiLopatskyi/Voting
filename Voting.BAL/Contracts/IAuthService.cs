using Voting.BAL.Models;
using Voting.DAL.Entities;

namespace Voting.BAL.Contracts
{
    public interface IAuthService
    {
        public Task<Result> RegisterAsync(RegisterDto model);
        public Task<LoginResult<string>> LoginAsync(LoginDto model);
    }

}
