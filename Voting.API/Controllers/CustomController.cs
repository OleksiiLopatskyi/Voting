using Microsoft.AspNetCore.Mvc;
using Voting.BAL.Models;

namespace Voting.API.Controllers
{
    public class CustomController : ControllerBase
    {
        protected string GetUserId()
        {
            var id = User.Claims.FirstOrDefault(i => i.Type == "Id").Value;
            return id;
        }
        protected IActionResult CustomResult(Result result)
        {
            switch (result.StatusCode)
            {
                case BAL.Models.StatusCode.InternalServerError:
                    return Ok(result);
                case BAL.Models.StatusCode.NotFound:
                    return NotFound(result);
                case BAL.Models.StatusCode.BadRequest:
                    return BadRequest(result);
                default: return Ok(result);
            }
        }
    }
}
