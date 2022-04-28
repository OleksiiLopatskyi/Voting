using Firebase.Auth;
using Firebase.Storage;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Voting.BAL.Contracts;
using Voting.BAL.Models;

namespace Voting.BAL.Services
{
    public class FireBaseClient : IFireBaseClient
    {
        private FireBaseOptions _fireBaseOptions;

        public FireBaseClient(IOptions<FireBaseOptions> options)
        {
            _fireBaseOptions = options.Value;
        }

        public async Task<Result<IEnumerable<string>>> UploadImages(IEnumerable<IFormFile> files)
        {
            var urlCollection = new List<string>();
            var cancellationToken = new CancellationTokenSource();
            var auth = new FirebaseAuthProvider(new FirebaseConfig(_fireBaseOptions.ApiKey));
            try
            {
                var signIn = await auth
                .SignInWithEmailAndPasswordAsync(_fireBaseOptions.Email, _fireBaseOptions.Password);

                foreach (var file in files)
                {
                    var upload = new FirebaseStorage(_fireBaseOptions.Bucket, new FirebaseStorageOptions()
                    {
                        AuthTokenAsyncFactory = () => Task.FromResult(signIn.FirebaseToken),
                        ThrowOnCancel = true
                    }).Child(Guid.NewGuid().ToString());
                    using (var stream = file.OpenReadStream())
                    {
                        await upload.PutAsync(stream, cancellationToken.Token);
                    }
                    urlCollection.Add(await upload.GetDownloadUrlAsync());
                }
                return new Result<IEnumerable<string>> { Data = urlCollection };
            }
            catch (Exception ex)
            {
                return new Result<IEnumerable<string>>
                {
                    Message = ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
    }
}
