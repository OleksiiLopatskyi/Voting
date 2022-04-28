using Voting.DAL.Entities;

namespace Voting.BAL.Builders
{
    public interface IAccountBuilder
    {
        public User Account { get; }
        public IAccountBuilder WithEmail(string email);
        public IAccountBuilder WithRole(Role role);
        public IAccountBuilder WithUsername(string username);
        public IAccountBuilder WithPassword(string password);
        public User Build();
    }
}
