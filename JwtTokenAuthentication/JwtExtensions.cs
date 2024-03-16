using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace JwtTokenAuthentication;
public static class JwtExtensions
{
    public const string SecurityKey = "portalMuralhaPaulistaSSPJwt@secretK";

    public static void AddJwtAuthentication(this IServiceCollection services)
    {
        services.AddAuthentication(opt => {
            opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            string iss;
            switch (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"))
            {
                case "Development":
                default:
                    iss = "https://localhost:34637";
                    break;
                case "Test":
                    iss = "http://ssp.corpssp.web.test";
                    break;
                case "Homolog":
                    iss = "http://ssp.corpssp.web.hml";
                    break;
                case "Prod":
                    iss = "http://back_corpssp";
                    break;
            }
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = iss,
                ValidateAudience = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecurityKey))
            };
        });
    }

}
