using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Voting.BAL.Models
{
    public enum StatusCode
    {
        Success = 0,
        NotFound = 1,
        InternalServerError = 2,
        BadRequest = 3
    }
}
