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
    public interface IPairService
    {
        Task<Result> GetAllAsync();
        Task<Result> CreateAsync();
        Task<Result> VoteAsync(VoteDto dto);
    }
}