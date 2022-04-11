using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Voting.BAL.Contracts;
using Voting.DAL.DTO;

namespace Voting.API.Controllers
{
    [Route("api/pairs")]
    public class PairsController : CustomController
    {
        private IPairService _modelsPairService;
        public PairsController(IPairService modelsPairService)
        {
            _modelsPairService = modelsPairService;
        }

        [HttpGet]
        public async Task<IActionResult> GetPairs()
        {
            var result = await _modelsPairService.GetAllAsync();
            return CustomResult(result);
        }
        [HttpPost]
        public async Task<IActionResult> CreatePairs()
        {
            var result = await _modelsPairService.CreateAsync();
            return CustomResult(result);
        }
        [HttpPut]
        public async Task<IActionResult> Vote(VoteDto dto)
        {
            var result = await _modelsPairService.VoteAsync(dto);
            return CustomResult(result);
        }
    }
}
