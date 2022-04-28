using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Voting.DAL.Context;
using Voting.DAL.Contracts;
using Voting.DAL.Entities;

namespace Voting.DAL.Repository
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(DatabaseContext databaseContext) : base(databaseContext)
        {
        }
        public async Task<User> FindEntityAsync(Expression<Func<User, bool>> expression)
        {
            return await DataContext.Set<User>()
                .Include(u=>u.FavoriteModel).ThenInclude(u=>u.Images)
                .Include(u=>u.Pairs).ThenInclude(p=>p.FirstModel)
                .Include(u=>u.Pairs).ThenInclude(p=>p.SecondModel)
                .FirstOrDefaultAsync(expression);
        }
    }
}
