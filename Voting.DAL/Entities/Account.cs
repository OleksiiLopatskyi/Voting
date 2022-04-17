using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Voting.DAL.Entities
{
    public class Account
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public ICollection<Pair> Pairs { get; set; }
        public int RoleId { get; set; }
        public Role Role { get; set; }
        public Account()
        {
            Pairs = new List<Pair>();
        }
    }
}
