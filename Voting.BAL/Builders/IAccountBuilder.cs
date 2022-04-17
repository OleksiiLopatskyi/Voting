using Voting.DAL.Entities;

namespace Voting.BAL.Builders
{
    public interface IAccountBuilder
    {
        public Account Account { get; }
        public IAccountBuilder WithEmail(string email);
        public IAccountBuilder WithRole(Role role);
        public IAccountBuilder WithProfile(Profile profile);
        public IAccountBuilder WithUsername(string password);
        public IAccountBuilder WithPassword(string password);
        public Account Build();
    }
}
