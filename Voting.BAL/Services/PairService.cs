using Microsoft.AspNetCore.Authorization;
using Voting.BAL.Attributes;
using Voting.BAL.Contracts;
using Voting.BAL.Extensions;
using Voting.BAL.Models;
using Voting.DAL.Contracts;
using Voting.DAL.Entities;

namespace Voting.BAL.Services
{
    public class PairService : IPairService
    {
        private IUnitOfWork _unitOfWork;
        public PairService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<Pair>> GetNoVotedPairAsync(string accountId)
        {
            var pair = await _unitOfWork.ModelsPairRepository
                .FindEntityAsync
                (i => i.UserId == accountId && i.IsVoted == false);
            return new Result<Pair> { Data = pair };
        }
        public async Task<Result<Pair>> VoteAsync(string userId, VoteDto dto)
        {
            try
            {
                var pair = await _unitOfWork.ModelsPairRepository
                    .FindEntityAsync(p => p.Id == dto.PairId);
                var winner = pair.FirstModelId == dto.WinnerId 
                    ? pair.FirstModel 
                    : pair.SecondModel;
                var loser = pair.FirstModelId == dto.WinnerId 
                    ? pair.SecondModel 
                    : pair.FirstModel;
                winner.ShowTimes += 1;
                winner.VotesCount += 1;
                loser.ShowTimes += 1;
                pair.IsVoted = true;
                pair.WinnerId = winner.Id;
                _unitOfWork.ModelRepository.Update(winner);
                _unitOfWork.ModelRepository.Update(loser);
                _unitOfWork.ModelsPairRepository.Update(pair);
                var user = await _unitOfWork.UserRepository
                    .FindEntityAsync(u=>u.Id==userId);
                var model = await SetFavoriteModelAsync(user.Pairs);
                user.FavoriteModel = model;
                await _unitOfWork.SaveAsync();
                var notVotedPair = await GetNoVotedPairAsync(userId);
                return new Result<Pair> { Data = notVotedPair.Data };
            }
            catch (Exception ex)
            {
                return new Result<Pair> { StatusCode = StatusCode.InternalServerError };
            }
        }

        private async Task<Model> SetFavoriteModelAsync(IEnumerable<Pair> pairs)
        {
            if (pairs.Count() < 1 || pairs == null)
            {
                return null;
            }
            var votedPairs = pairs.Where(p => p.IsVoted);
            var dict = new Dictionary<Model, int>();
            foreach (var pair in votedPairs)
            {
                var winner = pair.WinnerId == pair.FirstModelId
                    ? pair.FirstModel
                    : pair.SecondModel;
                if (dict.ContainsKey(winner))
                {
                    dict[winner]++;
                }
                else
                {
                    dict.Add(winner, 1);
                }
            }
            var favModel =
                dict.FirstOrDefault(x => x.Value == dict.Values.Max()).Key;
            return favModel;
        }

        public async Task<IEnumerable<Pair>> GeneratePairsAsync()
        {
            var models = await _unitOfWork.ModelRepository.FindAllAsync();
            var pairs = new List<Pair>();
            var tempoList = new List<int>();
            for (int i = 0; i < models.Count(); i++)
            {
                tempoList.Add(models.ElementAt(i).Id);
                for (int j = 0; j < models.Count(); j++)
                {
                    if (j != i && !tempoList.Contains(models.ElementAt(j).Id))
                    {
                        var pair = new Pair
                        {
                            FirstModel = models.ElementAt(i),
                            SecondModel = models.ElementAt(j),
                            IsVoted = false
                        };
                        pairs.Add(pair);
                    }
                }
            }
            return pairs.Shuffle();
        }

        public async Task<Result> DeleteAsync(int id)
        {
            try
            {
                var entity = await _unitOfWork.ModelsPairRepository
                    .FindEntityAsync(i => i.Id == id);
                _unitOfWork.ModelsPairRepository.Delete(entity);
                await _unitOfWork.SaveAsync();
                return new Result<Pair> { Data = entity };
            }
            catch (Exception ex)
            {
                return new Result { StatusCode = StatusCode.InternalServerError };
            }

        }

        public async Task<Result> GetNewPairs(string userId)
        {
            var newPairs = await GeneratePairsAsync();
            var oldPairs = await _unitOfWork.ModelsPairRepository
                .FindAllByConditionAsync(p => p.UserId == userId);
            var union = newPairs.Union(oldPairs);
            var result = union.Distinct();
            return new Result<IEnumerable<Pair>> { Data = result };
        }

        public async Task<Result<Pair>> ResetVotesAsync(string userId)
        {
            try
            {
                var pairs = await _unitOfWork.ModelsPairRepository
                            .FindAllByConditionAsync(p => p.UserId == userId);
                foreach (var item in pairs.Where(i=>i.IsVoted))
                {
                    item.IsVoted = false;
                    item.FirstModel.ShowTimes -= 1;
                    item.SecondModel.ShowTimes -= 1;
                    if (item.FirstModelId == item.WinnerId)
                    {
                        item.FirstModel.VotesCount -= 1;
                    }
                    else
                    {
                        item.SecondModel.VotesCount -= 1;
                    }
                    _unitOfWork.ModelRepository.Update(item.FirstModel);
                    _unitOfWork.ModelRepository.Update(item.SecondModel);
                    _unitOfWork.ModelsPairRepository.Update(item);
                }
                var generatedPairs = await GeneratePairsAsync();
                var user = await _unitOfWork.UserRepository.FindEntityAsync(i => i.Id == userId);
                user.Pairs = generatedPairs.ToList();
                _unitOfWork.UserRepository.Update(user);
                await _unitOfWork.SaveAsync();
                var notVotedPair = await GetNoVotedPairAsync(userId);
                return new Result<Pair>
                {
                    Data = notVotedPair.Data,
                    StatusCode = StatusCode.Success
                };
            }
            catch (Exception ex)
            {
                return new Result<Pair>
                {
                    Message = ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
    }
}
