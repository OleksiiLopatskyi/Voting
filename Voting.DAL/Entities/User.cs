using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Voting.DAL.Entities
{
    public enum UserRoles
    {
        Admin,
        User
    }
    public class User : IdentityUser
    {
        public string? AvatarUrl { get; set; }
        public bool Active { get; set; }
        public UserRoles Role { get; set; }
        public ICollection<Pair> Pairs { get; set; }

        [ForeignKey("Model")]
        public int? FavoriteModelId { get; set; }
        public Model FavoriteModel { get; set; }

        public User()
        {
            Pairs = new List<Pair>();
        }
    }
}
