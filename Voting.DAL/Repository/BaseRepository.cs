using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Voting.DAL.Context;
using Voting.DAL.Contracts;

namespace Voting.DAL.Repository
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected DatabaseContext DataContext;
        public BaseRepository(DatabaseContext databaseContext)
        {
            DataContext = databaseContext;
        }

        public async Task CreateAsync(T entity)
        {
            await DataContext.Set<T>().AddAsync(entity);
        }

        public void Delete(T entity)
        {
            DataContext.Set<T>().Remove(entity);
        }

        public async Task<IEnumerable<T>> FindAllAsync()
        {
            return await DataContext.Set<T>().AsNoTracking().ToListAsync();
        }

        public void Update(T entity)
        {
            DataContext.Set<T>().Update(entity);
        }

        public async Task<T> FindEntityAsync(Expression<Func<T, bool>> expression)
        {
            return await DataContext.Set<T>().FirstOrDefaultAsync(expression); 
        }
    }
}
