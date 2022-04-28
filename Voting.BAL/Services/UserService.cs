using Microsoft.AspNetCore.Http;
using Voting.BAL.Contracts;
using Voting.BAL.Models;
using Voting.DAL.Contracts;
using Voting.DAL.Entities;

namespace Voting.BAL.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFireBaseClient _client;
        public UserService(IUnitOfWork unitOfWork, IFireBaseClient client)
        {
            _unitOfWork = unitOfWork;
            _client = client;
        }

        public async Task<Result<IEnumerable<UserProfileDto>>> GetAllAsync(string userId)
        {
            try
            {
                var users = await _unitOfWork.UserRepository
                    .FindAllByConditionAsync(i => i.Role != 0 && i.Id != userId);
                var table = users.Select(u => new UserProfileDto
                {
                    Id = u.Id,
                    Image = u.AvatarUrl,
                    Email = u.Email,
                    Username = u.UserName,
                    Active = u.Active
                });
                return new Result<IEnumerable<UserProfileDto>> { Data = table };
            }
            catch (Exception ex)
            {
                return new Result<IEnumerable<UserProfileDto>>
                {
                    Message = ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<Result<User>> GetUserAsync(string id)
        {
            try
            {
                var user = await _unitOfWork.UserRepository.FindEntityAsync(u => u.Id == id);
                return new Result<User> { Data = user };
            }
            catch (Exception ex)
            {
                return new Result<User>
                {
                    Message = ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<Result<IEnumerable<ModelTableDto>>> GetVotingStatsAsync(string id)
        {
            try
            {
                var pairs = await _unitOfWork.ModelsPairRepository
                    .FindAllByConditionAsync(p => p.UserId == id && p.IsVoted);
                var table = await GenerateVotingStats(pairs);
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
        public async Task<Result<User>> UpdateAsync(string id, UserDto dto)
        {
            try
            {
                var user = await _unitOfWork.UserRepository
                    .FindEntityAsync(u => u.Id == id);
                if (user == null)
                {
                    return new Result<User>
                    {
                        Message = "Користувача не знайдено",
                        StatusCode = StatusCode.NotFound
                    };
                }
                if(dto.Avatar != null)
                {
                    var image = await _client.UploadImages(new List<IFormFile> { dto.Avatar });
                    if(image.Data == null)
                    {
                        return new Result<User> { Message = " Не загрузиласі картинка ", StatusCode = StatusCode.InternalServerError };
                    }
                    user.AvatarUrl = image.Data.First();
                }
                else
                {
                    user.AvatarUrl = String.Empty;   
                }
                if (!String.IsNullOrEmpty(dto.Username))
                {
                    user.UserName = dto.Username;
                }
                _unitOfWork.UserRepository.Update(user);
                await _unitOfWork.SaveAsync();
                return new Result<User>
                {
                    Data = user,
                    Message = "Користувача оновлено",
                    StatusCode = StatusCode.Success
                };
            }
            catch (Exception ex)
            {
                return new Result<User>
                {
                    Message = "Щось пішло не так",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IEnumerable<ModelTableDto>> GenerateVotingStats(IEnumerable<Pair> pairs)
        {
            var models = await _unitOfWork.ModelRepository.FindAllAsync();
            var votedPairs = pairs.Where(p => p.IsVoted);
            var dict = new Dictionary<Model, int>();
            foreach (var item in models)
            {
                dict.Add(item, 0);
            }
            foreach (var pair in votedPairs)
            {
                var winner = pair.WinnerId == pair.FirstModelId
                    ? pair.FirstModel
                    : pair.SecondModel;
                if (dict.ContainsKey(winner))
                {
                    dict[winner]++;
                }
            }
            var table = dict.Select(p => new ModelTableDto
            {
                Image = p.Key.Images.First().Url,
                Name = p.Key.Name,
                VotesCount = p.Value
            });
            return table.OrderByDescending(m => m.VotesCount);
        }

        public async Task<Result<UserProfileDto>> GetProfileAsync(string userId)
        {
            try
            {
                var user = await _unitOfWork.UserRepository
                    .FindEntityAsync(i => i.Id == userId && i.FavoriteModel != null);
                if (user == null)
                {
                    return new Result<UserProfileDto>
                    {
                        Message = "Користувача не існує",
                        StatusCode = StatusCode.BadRequest
                    };
                }
                var userPairs = await GetVotingStatsAsync(userId);
                var profile = new UserProfileDto
                {
                    Username = user.UserName,
                    Image = user.AvatarUrl,
                    FavoriteModel = user.FavoriteModel.Name,
                    FavoriteModelImage = user.FavoriteModel.Images.First().Url,
                    UserVotesTable = userPairs.Data
                };
                return new Result<UserProfileDto> { Data = profile };

            }
            catch (Exception ex)
            {
                return new Result<UserProfileDto>
                {
                    Message = ex.Message + $"\n Inner:{ex.InnerException.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<Result> DeleteAsync(string userId)
        {
            try
            {
                var user = await _unitOfWork.UserRepository.FindEntityAsync(u => u.Id == userId);
                _unitOfWork.UserRepository.Delete(user);
                await _unitOfWork.SaveAsync();
                return new Result
                {
                    Message = "Користувача успішно видалено",
                    StatusCode = StatusCode.Success
                };
            }
            catch (Exception ex)
            {
                return new Result { Message = "Щось пішло не так :(", StatusCode = StatusCode.InternalServerError };
            }

        }
    }
}
