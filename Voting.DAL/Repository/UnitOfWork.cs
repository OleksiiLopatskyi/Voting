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
        private IRoleRepository _roleRepository;
        private IUserRepository _userRepository;
        private IImageRepository _imageRepository;
        public UnitOfWork(DatabaseContext databaseContext)
        {
            _dataContext = databaseContext;
        }

        public IImageRepository ImageRepository
        {
            get
            {
                if (_imageRepository == null)
                    return new ImageRepository(_dataContext);
                return _imageRepository;
            }
        }
        public IUserRepository UserRepository
        {
            get
            {
                if (_userRepository == null)
                    _userRepository = new UserRepository(_dataContext);
                return _userRepository;
            }
        }
        public IRoleRepository RoleRepository
        {
            get
            {
                if (_roleRepository == null)
                {
                    _roleRepository = new RoleRepository(_dataContext);
                }
                return _roleRepository;
            }
        }
        public IModelRepository ModelRepository
        {
            get
            {
                if (_modelRepository == null)
                    _modelRepository = new ModelRepository(_dataContext);
                return _modelRepository;
            }
        }
        public IPairRepository ModelsPairRepository
        {
            get
            {
                if (_modelsPairRepository == null)
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
