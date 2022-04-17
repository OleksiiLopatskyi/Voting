using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Voting.DAL.Entities
{
    public class Profile
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public string Username { get; set; }
        public virtual ICollection<Pair> Pairs { get; set; }
    }
}
