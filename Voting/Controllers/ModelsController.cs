using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Voting.BAL.Contracts;
using Voting.DAL.DTO;

namespace Voting.Controllers
{
    [Route("api/models")]
    [ApiController]
    public class ModelsController : ControllerBase
    {
        private IModelService _modelService;

        public ModelsController(IModelService modelService)
        {
            _modelService = modelService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllModels()
        {
            return Ok(await _modelService.GetAllModels());
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetModel(int id)
        {
            return Ok(await _modelService.GetModelById(id));
        }
        [HttpPost]
        public async Task<IActionResult> Create(ModelDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(model);
            }
            var result = await _modelService.Create(model);
            return Ok(result);
        }
    }
}
