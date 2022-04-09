using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Voting.BAL.Models;
using Voting.DAL.DTO;
using Voting.DAL.Entities;

namespace Voting.BAL.Contracts
{
    public interface IModelService
    {
        Task<Result> GetAllModels();
        Task<Result> GetModelById(int id);
        Task<Result> Create(ModelDto model);
        Task<Result> Update(ModelDto model);
        Task<Result> Delete(int id);
    }
}
