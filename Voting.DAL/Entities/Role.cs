using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Voting.DAL.Entities
{
    public class Role : IdentityRole
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<User> Accounts { get; set; }
        public Role()
        {
            Accounts = new List<User>();
        }
    }
}
