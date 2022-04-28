using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Voting.BAL.Models;

namespace Voting.BAL.Contracts
{
    public interface IAdminService
    {
        public Task<Result> SwitchStatusAsync(string userId);
    }
}
