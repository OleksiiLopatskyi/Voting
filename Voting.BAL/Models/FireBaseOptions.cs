using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Voting.BAL.Models
{
    public class FireBaseOptions
    {
        public string ApiKey { get; set; }
        public string Bucket { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
