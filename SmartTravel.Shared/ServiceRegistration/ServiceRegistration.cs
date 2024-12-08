using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using SmartTravel.Shared.Authentication;
using SmartTravel.Shared.Logging;
using SmartTravel.Shared.Middleware;

namespace SmartTravel.Shared.ServiceRegistration
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddSharedServices<TContext>(this IServiceCollection services, IConfiguration config, string fileName) where TContext : DbContext
        {
            services.AddDbContext<TContext>(options =>
            {
                options.UseSqlServer(config.GetConnectionString("SmartTravel"), sqlServerOption =>
                    {
                        sqlServerOption.EnableRetryOnFailure();
                    }
                );
            });

            Log.Logger = LoggingRegistration.CreateLogger(fileName);

            JwtAuthentication.AddJwtAuthentication(services, config);

            return services;
        }

        public static IApplicationBuilder UseSharedPolicies(this IApplicationBuilder app)
        {
            app.UseMiddleware<GlobalException>();

            //app.UseMiddleware<ServiceApiException>();

            return app;
        }
    }
}
