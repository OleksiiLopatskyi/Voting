using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Voting.BAL.Models
{
    public class UserProfileDto
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Image { get; set; }
        public string FavoriteModel { get; set; }
        public string FavoriteModelImage { get; set; }
        public bool Active { get; set; }
        public IEnumerable<ModelTableDto> UserVotesTable { get; set; }
    }
}
