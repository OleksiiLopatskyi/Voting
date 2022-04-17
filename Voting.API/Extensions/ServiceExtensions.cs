using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Voting.BAL.Contracts;
using Voting.BAL.Services;
using Voting.DAL.Contracts;
using Voting.DAL.Repository;

namespace Voting.API.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureJwt(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                var Key = Encoding.UTF8.GetBytes(configuration["JWT:Key"]);
                o.SaveToken = true;
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["JWT:Issuer"],
                    ValidAudience = configuration["JWT:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Key)
                };
            });

        }
        public static void ConfigureDependencies(this IServiceCollection services)
        {
            services.AddTransient<IModelService, ModelService>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IPairService, PairService>();
            services.AddTransient<IFireBaseClient, FireBaseClient>();
            services.AddTransient<IAccountService, AccountService>();
        }
    }
}
