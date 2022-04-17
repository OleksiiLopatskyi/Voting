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
        private IPairRepository _modelsPairRepository;
        private IAccountRepository _accountRepository;
        private IRoleRepository _roleRepository;
        public UnitOfWork(DatabaseContext databaseContext)
        {
            _dataContext = databaseContext;
        }
        public IRoleRepository RoleRepository {
            get 
            {
                if (_roleRepository == null)
                {
                   _roleRepository = new RoleRepository(_dataContext);
                }
                return _roleRepository;
            }
        }
        public IAccountRepository AccountRepository { 
            get
            {
                if (_accountRepository == null)
                    _accountRepository = new AccountRepository(_dataContext);
                return _accountRepository;
            } 
        }
        public IModelRepository ModelRepository { 
            get
            {
                if(_modelRepository == null)
                    _modelRepository = new ModelRepository(_dataContext);
                return _modelRepository;
            }
        }
        public IPairRepository ModelsPairRepository { 
            get 
            {
                if(_modelsPairRepository==null)
                    return new PairRepository(_dataContext);
                return _modelsPairRepository;
            } 
        }

        public async Task SaveAsync()
        {
            await _dataContext.SaveChangesAsync();
        }
    }
}
