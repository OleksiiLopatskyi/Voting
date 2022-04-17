using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Voting.DAL.Context;
using Voting.DAL.Contracts;
using Voting.DAL.Entities;

namespace Voting.DAL.Repository
{
    public class AccountRepository : BaseRepository<Account>, IAccountRepository
    {
        public AccountRepository(DatabaseContext databaseContext) : base(databaseContext)
        {
        }
        public new async Task<Account> FindEntityAsync(Expression<Func<Account, bool>> expression)
        {
            return await DataContext.Set<Account>()
                .Include(a=>a.Profile)
                .ThenInclude(a=>a.Pairs)
                .Include(a=>a.Role)
                .FirstOrDefaultAsync(expression);
        }
    }
}
