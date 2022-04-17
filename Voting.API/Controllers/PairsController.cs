using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Voting.BAL.Contracts;
using Voting.BAL.Models;

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
        [Authorize]
        [HttpGet("getPair")]
        public async Task<IActionResult> GetPair()
        {
            var result = await _modelsPairService.GetNoVotedPairAsync(GetAccountId());
            return CustomResult(result);
        }
        [HttpPost]
        public async Task<IActionResult> CreatePairs()
        {
            var result = await _modelsPairService.CreateAsync(GetAccountId());
            return CustomResult(result);
        }
        [HttpPut("vote")]
        public async Task<IActionResult> Vote([FromBody]VoteDto dto)
        {
            var result = await _modelsPairService.VoteAsync(dto);
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
