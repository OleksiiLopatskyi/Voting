using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Voting.DAL.DTO
{
    public class ModelDto
    {
        public string Name { get; set; }
        public IEnumerable<string> Images { get; set; }
    }
}
