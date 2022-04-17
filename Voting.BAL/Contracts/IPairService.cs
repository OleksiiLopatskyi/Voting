using Voting.BAL.Models;

namespace Voting.BAL.Contracts
{
    public interface IPairService
    {
        Task<Result> GetNoVotedPairAsync(int accountId);
        Task<Result> CreateAsync(int accountId);
        Task<Result> VoteAsync(VoteDto dto);
        Task<Result> DeleteAsync(int id);
    }
}