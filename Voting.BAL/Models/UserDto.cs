using Microsoft.AspNetCore.Http;

namespace Voting.BAL.Models
{
    public class UserDto
    {
        public string? Username { get; set; }
        public IFormFile? Avatar { get; set; }
    }
}
