using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Voting.DAL.Entities;

namespace Voting.DAL.Contracts
{
    public interface IModelRepository : IBaseRepository<Model>
    {
    }
}
