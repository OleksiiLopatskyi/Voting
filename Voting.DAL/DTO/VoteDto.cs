using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Voting.DAL.DTO
{
    public class VoteDto
    {
        public int PairId { get; set; }
        public int WinnerId { get; set; }
    }
}
