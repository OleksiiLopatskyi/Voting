using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Voting.DAL.Context;
using Voting.DAL.Contracts;

namespace Voting.DAL.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private DatabaseContext _dataContext;
        private IModelRepository _modelRepository;

        public UnitOfWork(DatabaseContext databaseContext)
        {
            _dataContext = databaseContext;
        }
        public IModelRepository ModelRepository { 
            get
            {
                if(_modelRepository == null)
                    _modelRepository = new ModelRepository(_dataContext);
                return _modelRepository;
            }
        }

        public async Task SaveAsync()
        {
            await _dataContext.SaveChangesAsync();
        }
    }
}
