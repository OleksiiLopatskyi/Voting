using Microsoft.AspNetCore.Mvc;
using Voting.BAL.Contracts;

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
        [HttpGet]
        public async Task<IActionResult> GetAllModels()
        {
            var result = await _modelService.GetAllModels();
            return CustomResult(result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetModel(int id)
        {
            var result = await _modelService.GetModelById(id);
            return CustomResult(result);
        }
        [HttpPost]
        public async Task<IActionResult> CreateModel(IFormCollection form)
        {
            var result = await _modelService.Create(form);
            return CustomResult(result);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _modelService.DeleteAsync(id);
            return CustomResult(result);
        }
    }
}
