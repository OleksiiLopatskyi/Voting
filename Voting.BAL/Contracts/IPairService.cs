using Voting.BAL.Models;
using Voting.DAL.Entities;

namespace Voting.BAL.Contracts
{
    public interface IPairService
    {
        Task<Result> GetNewPairs(string userId);
        Task<Result<Pair>> GetNoVotedPairAsync(string userId);
        Task<IEnumerable<Pair>> GeneratePairsAsync();
        Task<Result<Pair>> VoteAsync(string userId, VoteDto dto);
        Task<Result<Pair>> ResetVotesAsync(string userId);
        Task<Result> DeleteAsync(int id);
    }
}