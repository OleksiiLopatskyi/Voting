using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Voting.BAL.Models
{
    public class Result<T> : Result
    {
        public T Data { get; set; }
    }
    public class Result
    {
        public string Message { get; set; }
        public StatusCode StatusCode { get; set; }
    }
}
