using Microsoft.AspNetCore.Http;
using Voting.BAL.Builders;
using Voting.BAL.Contracts;
using Voting.BAL.Models;
using Voting.DAL.Contracts;
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

        public async Task<Result> Create(ModelDto dto)
        {
            try
            {
                var images = await _firebaseClient.UploadImages(dto.Images);
                if (images.Data == null)
                {
                    return images;
                }
                var model = new ModelBuilder()
                    .Map(dto)
                    .WithImages(images.Data)
                    .Build();

                await _unitOfWork.ModelRepository.CreateAsync(model);
                await _unitOfWork.SaveAsync();
                return new Result<Model>
                {
                    Data = model,
                    StatusCode = StatusCode.Created
                };
            }
            catch (Exception ex)
            {
                return new Result
                {
                    Message = "Маленьку(Малячку, Малиху) не створено",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
        public async Task<Result> GetAllModels()
        {
            var models = await _unitOfWork.ModelRepository.FindAllAsync();

            return new Result<IEnumerable<Model>> { Data = models };
        }

        public async Task<Result> GetModelById(int id)
        {
            var model = await _unitOfWork.ModelRepository
                .FindEntityAsync(m => m.Id == id);
            if (model == null)
            {
                return new Result
                {
                    StatusCode = StatusCode.NotFound
                };
            }
            return new Result<Model> { Data = model };
        }

        public Task<Result> Update(ModelDto model)
        {
            throw new NotImplementedException();
        }
        public async Task<Result> DeleteAsync(int id)
        {
            try
            {
                var users = await _unitOfWork.UserRepository
                    .FindAllByConditionAsync(m => m.FavoriteModelId == id);
                var pairs = await _unitOfWork.ModelsPairRepository
                    .FindAllByConditionAsync(m => m.FirstModelId == id || m.SecondModelId == id);
                var images = await _unitOfWork.ImageRepository
                    .FindAllByConditionAsync(i=>i.ModelId == id);
                _unitOfWork.ImageRepository.DeleteRange(images);
                _unitOfWork.ModelsPairRepository.DeleteRange(pairs);
                foreach (var user in users)
                {
                    user.FavoriteModel = null;
                    _unitOfWork.UserRepository.Update(user);
                }
                var entity = await _unitOfWork.ModelRepository
                    .FindEntityAsync(m => m.Id == id);
                if (entity == null)
                {
                    return new Result
                    {
                        StatusCode = StatusCode.NotFound
                    };
                }
                _unitOfWork.ModelRepository.Delete(entity);
                await _unitOfWork.SaveAsync();
                return new Result<Model> { Data = entity };
            }
            catch (Exception ex)
            {
                return new Result { StatusCode = StatusCode.InternalServerError };
            }
        }

        public async Task<Result<IEnumerable<ModelTableDto>>> GetVotingResultAsync()
        {
            try
            {
                var models = await _unitOfWork.ModelRepository.FindAllAsync();
                var table = models.Select(x => new ModelTableDto
                {
                    Image = x.Images.First().Url,
                    Name = x.Name,
                    Rating = x.Rating
                });
                return new Result<IEnumerable<ModelTableDto>> { Data = table };
            }
            catch (Exception ex)
            {
                return new Result<IEnumerable<ModelTableDto>>
                {
                    Message = "Щось пішло не так :(",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
    }
}
