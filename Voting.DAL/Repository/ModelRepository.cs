using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Voting.DAL.Context;
using Voting.DAL.Contracts;
using Voting.DAL.Entities;

namespace Voting.DAL.Repository
{
    public class ModelRepository :BaseRepository<Model>, IModelRepository
    {
        public ModelRepository(DatabaseContext databaseContext) : base(databaseContext)
        {
        }
    }
}
