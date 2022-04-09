using Voting.BAL.Builders;
using Voting.BAL.Contracts;
using Voting.BAL.Models;
using Voting.DAL.Contracts;
using Voting.DAL.DTO;
using Voting.DAL.Entities;

namespace Voting.BAL.Services
{
    public class ModelService : IModelService
    {
        private IUnitOfWork _unitOfWork;

        public ModelService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Create(ModelDto dto)
        {
            var model = new ModelBuilder()
                .WithName(dto.Name)
                .WithImages(dto.Images)
                .Build();
            try
            {
                await _unitOfWork.ModelRepository.CreateAsync(model);
                await _unitOfWork.SaveAsync();
                return new GenericResult<Model> { Data = model };
            }
            catch (Exception ex)
            {
                return new Result { StatusCode = StatusCode.InternalServerError };
            }
        }

        public Task<Result> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Result> GetAllModels()
        {
            var models = await _unitOfWork.ModelRepository.FindAllAsync();
            return new GenericResult<IEnumerable<Model>> { Data = models };
        }

        public async Task<Result> GetModelById(int id)
        {
            var model = await _unitOfWork.ModelRepository.FindEntityAsync(m => m.Id == id);
            return new GenericResult<Model> { Data = model };
        }

        public Task<Result> Update(ModelDto model)
        {
            throw new NotImplementedException();
        }
    }
}
