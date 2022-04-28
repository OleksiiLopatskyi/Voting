using Voting.BAL.Models;
using Voting.DAL.Entities;

namespace Voting.BAL.Contracts
{
    public interface IUserService
    {
        public Task<Result<User>> GetUserAsync(string id);

        public Task<Result<IEnumerable<UserProfileDto>>> GetAllAsync(string userId);

        public Task<Result<IEnumerable<ModelTableDto>>> GetVotingStatsAsync(string id);

        public Task<Result<User>> UpdateAsync(string id, UserDto dto);
        Task<Result<UserProfileDto>> GetProfileAsync(string userId);
        Task<Result> DeleteAsync(string userId);
    }
}
