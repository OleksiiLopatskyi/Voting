using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Voting.DAL.Entities;

namespace Voting.BAL.Builders
{
    public interface IModelBuilder
    {
        IModelBuilder Map(IFormCollection form);
        IModelBuilder WithImages(IEnumerable<string> images);
        Model Build();
    }
}
