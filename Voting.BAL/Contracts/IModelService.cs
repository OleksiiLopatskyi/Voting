using Microsoft.AspNetCore.Http;
using Voting.BAL.Models;

namespace Voting.BAL.Contracts
{
    public interface IModelService
    {
        Task<Result> GetAllModels();
        Task<Result> GetModelById(int id);
        Task<Result> Create(IFormCollection form);
        Task<Result> Update(ModelDto model);
        Task<Result> DeleteAsync(int id);
    }
}
