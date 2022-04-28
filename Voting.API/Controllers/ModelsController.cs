using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Voting.BAL.Attributes;
using Voting.BAL.Contracts;
using Voting.BAL.Models;
using Voting.DAL.Entities;

namespace Voting.API.Controllers
{
    [Route("api/models")]
    [ApiController]
    public class ModelsController : CustomController
    {
        private IModelService _modelService;
        public ModelsController(IModelService modelService)
        {
            _modelService = modelService;
        }

        [MyAuthorize(Role = UserRoles.Admin)]
        [HttpGet]
        public async Task<IActionResult> GetAllModels()
        {
            var result = await _modelService.GetAllModels();
            return CustomResult(result);
        }

        [MyAuthorize(Role = UserRoles.User)]
        [HttpGet("voting-result")]
        public async Task<IActionResult> GetVotingResult()
        {
            var result = await _modelService.GetVotingResultAsync();
            return CustomResult(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetModel(int id)
        {
            var result = await _modelService.GetModelById(id);
            return CustomResult(result);
        }

        [MyAuthorize(Role = UserRoles.Admin)]
        [HttpPost]
        public async Task<IActionResult> CreateModel([FromForm]ModelDto dto)
        {
            var result = await _modelService.Create(dto);
            return CustomResult(result);
        }

        [MyAuthorize(Role = UserRoles.Admin)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _modelService.DeleteAsync(id);
            return CustomResult(result);
        }
    }
}
