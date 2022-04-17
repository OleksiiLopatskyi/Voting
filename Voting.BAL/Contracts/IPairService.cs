using Voting.BAL.Models;

namespace Voting.BAL.Contracts
{
    public interface IPairService
    {
        Task<Result> GetNoVotedPairAsync(int profileId);
        Task<Result> CreateAsync(int profileId);
        Task<Result> VoteAsync(VoteDto dto);
        Task<Result> DeleteAsync(int id);
    }
}