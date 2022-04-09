using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Voting.DAL.Contracts
{
    public interface IUnitOfWork
    {
        IModelRepository ModelRepository { get; }
        Task SaveAsync();
    }
}
