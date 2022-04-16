using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Voting.BAL.Contracts;
using Voting.BAL.Models;
using Voting.DAL.Contracts;
using Voting.DAL.DTO;
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
        public async Task<Result> GetNoVotedPairAsync()
        {
            try
            {
                var pair = await _unitOfWork.ModelsPairRepository.FindEntityAsync(p => p.IsVoted == false); ;
                return new GenericResult<Pair> { Data = pair };
            }
            catch (Exception ex)
            {
                return new Result { StatusCode = StatusCode.InternalServerError };
            }
        }
        public async Task<Result> CreateAsync()
        {
            try
            {
                var generatedPairs = await GeneratePairsAsync();
                foreach (var pair in generatedPairs)
                {
                    await _unitOfWork.ModelsPairRepository.CreateAsync(pair);
                }
                await _unitOfWork.SaveAsync();
                return new GenericResult<IEnumerable<Pair>> { Data = generatedPairs };
            }
            catch (Exception ex)
            {
                return new Result { StatusCode = StatusCode.InternalServerError };
            }
        }
        public async Task<Result> VoteAsync(VoteDto dto)
        {
            try
            {
                var pair = await _unitOfWork.ModelsPairRepository.FindEntityAsync(pair => pair.Id == dto.PairId);
                var winner = pair.FirstModelId == dto.PairId ? pair.FirstModel : pair.SecondModel;
                var loser = pair.FirstModelId == dto.PairId ? pair.SecondModel : pair.FirstModel;
                winner.ShowTimes++;
                winner.VotesCount++;
                loser.ShowTimes++;
                pair.IsVoted = true;
                _unitOfWork.ModelsPairRepository.Update(pair);
                _unitOfWork.ModelRepository.Update(winner);
                _unitOfWork.ModelRepository.Update(loser);
                await _unitOfWork.SaveAsync();
                return new GenericResult<Pair> { Data = pair };
            }
            catch (Exception ex)
            {
                return new Result { StatusCode = StatusCode.InternalServerError };
            }

        }
        private async Task<IEnumerable<Pair>> GeneratePairsAsync()
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
            return pairs;
        }

        public async Task<Result> DeleteAsync(int id)
        {
            try
            {
                var entity = await _unitOfWork.ModelsPairRepository.FindEntityAsync(i => i.Id == id);
                _unitOfWork.ModelsPairRepository.Delete(entity);
                await _unitOfWork.SaveAsync();
                return new GenericResult<Pair> { Data = entity };
            }
            catch (Exception ex)
            {
                return new Result { StatusCode = StatusCode.InternalServerError };
            }

        }
    }
}
