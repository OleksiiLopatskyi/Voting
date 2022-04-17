using Microsoft.AspNetCore.Mvc;
using Voting.BAL.Models;

namespace Voting.API.Controllers
{
    public class CustomController : ControllerBase
    {
        protected int GetAccountId()
        {
            return Int32.Parse(User.Claims.FirstOrDefault(i => i.Type == "Id").Value);
        }
        protected IActionResult CustomResult(Result result)
        {
            switch (result.StatusCode)
            {
                case BAL.Models.StatusCode.InternalServerError:
                    return StatusCode(StatusCodes.Status500InternalServerError);
                case BAL.Models.StatusCode.NotFound:
                   return NotFound(result);
                default: return Ok(result);
            }
        }   
    }
}
