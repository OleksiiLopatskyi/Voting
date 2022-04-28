using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Voting.DAL.Entities;

namespace Voting.BAL.Builders
{
    public class AccountBuilder : IAccountBuilder
    {
        private User _account;
        public AccountBuilder()
        {
            _account = new User();
        }
        public User Account => _account;

        public IAccountBuilder WithEmail(string email)
        {
            _account.Email = email;
            return this;
        }
        public IAccountBuilder WithRole(Role role)
        {
            return this;
        }
        public IAccountBuilder WithPassword(string password)
        {
            return this;
        }
        public IAccountBuilder WithUsername(string username)
        {
            return this;
        }
        public User Build()
        {
            return _account;
        }
    }
}
