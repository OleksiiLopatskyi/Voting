using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Voting.DAL.Entities
{
    public class Model
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int VotesCount { get; set; }
        public int ShowTimes { get; set; }
        public double Rating =>
            VotesCount == 0 || ShowTimes == 0 ? 0 : (VotesCount / ShowTimes) * 5;
        public ICollection<Image> Images { get; set; }
    }
}
