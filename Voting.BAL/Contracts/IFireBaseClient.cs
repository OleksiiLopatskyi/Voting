using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Voting.BAL.Contracts
{
    public interface IFireBaseClient
    {
        Task<IEnumerable<string>> UploadImages(IFormFileCollection files);
    }
}
