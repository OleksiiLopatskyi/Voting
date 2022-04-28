using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Voting.BAL.Models
{
    public class LoginResult<T> : Result<T> 
    {
        public string Role { get; set; }
    }
}
