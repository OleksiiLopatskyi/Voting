using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Voting.DAL.Contracts
{
    public interface IBaseRepository<T>
    {
        Task<IEnumerable<T>> FindAllAsync();
        Task<T> FindEntityAsync(Expression<Func<T, bool>> expression);
        Task<IEnumerable<T>> FindAllByConditionAsync(Expression<Func<T, bool>> expression);
        Task CreateAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
        void DeleteRange(IEnumerable<T> collection);

    }
}
