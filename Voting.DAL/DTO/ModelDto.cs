using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Voting.DAL.Entities;

namespace Voting.DAL.DTO
{
    public class ModelDto
    {
        [Required]
        public string Name { get; set; }
        public IEnumerable<string> Images { get; set; }
    }
}
