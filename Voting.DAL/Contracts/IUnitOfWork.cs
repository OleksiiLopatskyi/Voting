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
        IUserRepository UserRepository { get; }
        IPairRepository ModelsPairRepository { get; }
        IImageRepository ImageRepository { get; }

        Task SaveAsync();
    }
}
