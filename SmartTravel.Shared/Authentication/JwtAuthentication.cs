using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace SmartTravel.Shared.Authentication
{
    public static class JwtAuthentication
    {
        public static IServiceCollection AddJwtAuthentication (this IServiceCollection services, IConfiguration config)
        {
            services.AddAuthentication()
                .AddJwtBearer("Bearer", options =>
                {
                    var key = Encoding.UTF8.GetBytes(config.GetSection("Jwt:Key").Value!);
                    string issuer = config.GetSection("Jwt:Issuer").Value!;
                    string audience = config.GetSection("Jwt:Audience").Value!;

                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = false,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = issuer,
                        ValidAudience = audience,
                        IssuerSigningKey = new SymmetricSecurityKey(key)
                    };
                });

            return services;
        }
    }
}
