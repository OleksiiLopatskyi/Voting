using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Voting.DAL.Entities;

namespace Voting.DAL.Contracts
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<User> FindEntityAsync(Expression<Func<User, bool>> expression);
    }
}
