using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Voting.BAL.Attributes;
using Voting.BAL.Contracts;
using Voting.BAL.Models;
using Voting.DAL.Entities;

namespace Voting.API.Controllers
{
    [Authorize]
    [Route("api/pairs")]
    public class PairsController : CustomController
    {
        private IPairService _modelsPairService;
        public PairsController(IPairService modelsPairService)
        {
            _modelsPairService = modelsPairService;
        }
        [HttpGet("new-pairs")]
        public async Task<IActionResult> GetNewPairs()
        {
            var result = await _modelsPairService.GetNewPairs(GetUserId());
            return CustomResult(result);
        }
        [MyAuthorize(Role = UserRoles.User)]
        [HttpGet("getPair")]
        public async Task<IActionResult> GetPair()
        {
            var result = await _modelsPairService.GetNoVotedPairAsync(GetUserId());
            return CustomResult(result);
        }
        
        [MyAuthorize(Role = UserRoles.User)]
        [HttpPost("reset-votes")]
        public async Task<IActionResult> ResetVotes()
        {
            var result = await _modelsPairService.ResetVotesAsync(GetUserId());
            return CustomResult(result);
        }

        [MyAuthorize(Role = UserRoles.User)]
        [HttpPut("vote")]
        public async Task<IActionResult> Vote([FromBody]VoteDto dto)
        {
            var result = await _modelsPairService.VoteAsync(GetUserId(),dto);
            return CustomResult(result);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _modelsPairService.DeleteAsync(id);
            return CustomResult(result);
        }
    }
}
