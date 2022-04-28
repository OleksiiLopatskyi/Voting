using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Voting.BAL.Models;

namespace Voting.BAL.Contracts
{
    public interface IFireBaseClient
    {
        Task<Result<IEnumerable<string>>> UploadImages(IEnumerable<IFormFile> files);
    }
}
