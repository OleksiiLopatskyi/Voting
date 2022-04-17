using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Voting.DAL.Contracts
{
    public interface IUnitOfWork
    {
        IRoleRepository RoleRepository { get; }
        IModelRepository ModelRepository { get; }
        IPairRepository ModelsPairRepository { get; }
        IAccountRepository AccountRepository { get; }
        Task SaveAsync();
    }
}
