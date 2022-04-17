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
        private Account _account;
        public AccountBuilder()
        {
            _account = new Account();
        }
        public Account Account => _account;

        public IAccountBuilder WithEmail(string email)
        {
            _account.Email = email;
            return this;
        }
        public IAccountBuilder WithRole(Role role)
        {
            _account.Role = role;
            return this;
        }
        public IAccountBuilder WithPassword(string password)
        {
            _account.Password = password;
            return this;
        }
        public IAccountBuilder WithProfile(Profile profile)
        {
            _account.Profile = profile;
            return this;
        }
        public IAccountBuilder WithUsername(string username)
        {
            _account.Profile.Username = username;
            return this;
        }
        public Account Build()
        {
            return _account;
        }
    }
}
