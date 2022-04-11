using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Voting.BAL.Contracts;
using Voting.DAL.DTO;

namespace Voting.API.Controllers
{
    [Route("api/[controller]")]
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
        [HttpPost]
        public async Task<IActionResult> CreateModel(ModelDto model)
        {
            var result = await _modelService.Create(model);
            return CustomResult(result);
        }
    }
}
