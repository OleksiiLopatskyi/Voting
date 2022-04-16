using Microsoft.AspNetCore.Http;
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
        private IFireBaseClient _firebaseClient;
        public ModelService(IUnitOfWork unitOfWork, IFireBaseClient fireBaseClient)
        {
            _unitOfWork = unitOfWork;
            _firebaseClient = fireBaseClient;
        }

        public async Task<Result> Create(IFormCollection form)
        {
            try
            {
                var images = await _firebaseClient.UploadImages(form.Files);
                var model = new ModelBuilder()
                    .Map(form)
                    .WithImages(images)
                    .Build();

                await _unitOfWork.ModelRepository.CreateAsync(model);
                await _unitOfWork.SaveAsync();
                return new GenericResult<Model> { Data = model };
            }
            catch (Exception ex)
            {
                return new Result { StatusCode = StatusCode.InternalServerError };
            }
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
        public async Task<Result> DeleteAsync(int id)
        {
            try
            {
                var entity = await _unitOfWork.ModelRepository.FindEntityAsync(m => m.Id == id);
                _unitOfWork.ModelRepository.Delete(entity);
                await _unitOfWork.SaveAsync();
                return new GenericResult<Model> { Data = entity };
            }
            catch (Exception ex)
            {
                return new Result { StatusCode =StatusCode.InternalServerError };
            }
        }
    }
}
