using SmartTravel.Shared.ServiceRegistration;
using SmartTravel.UserService.DatabaseContext;
using SmartTravel.UserService.Helper.ModelMapping.Permission;
using SmartTravel.UserService.Helper.ModelMapping.Role;
using SmartTravel.UserService.Helper.ModelMapping.User;
using SmartTravel.UserService.Repositories;
using SmartTravel.UserService.Services;

namespace SmartTravel.UserService.Infrastructure
{
    public static class ServiceContainer
    {
        public static IServiceCollection AddInfrastructerServices(this IServiceCollection services, IConfiguration config)
        {
            ServiceRegistration.AddSharedServices<UserServiceDbContext>(services, config, config["MySerilog: FileName"]!);

            services.AddScoped<IUserBusinessLayer, UserBusinessLayer>();
            services.AddScoped<IRoleBusinessLayer, RoleBusinessLayer>();
            services.AddScoped<IPermissionBusinessLayer, PermissionBusinessLayer>();

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IPermissionRepository, PermissionRepository>();

            services.AddScoped<IUserMapping, UserMapping>();
            services.AddScoped<IRoleMapping, RoleMapping>();
            services.AddScoped<IPermissionMapping, PermissionMapping>();

            return services;
        }

        public static IApplicationBuilder AddInfrastructurePolicies(this IApplicationBuilder app)
        {
            ServiceRegistration.UseSharedPolicies(app);

            return app;
        }
    }
}
