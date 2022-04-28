using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Voting.BAL.Models
{
    public class ModelTableDto
    {
        public string Image { get; set; }
        public string Name { get; set; }
        public int VotesCount { get; set; }
        public double Rating { get; set; }
    }
}
