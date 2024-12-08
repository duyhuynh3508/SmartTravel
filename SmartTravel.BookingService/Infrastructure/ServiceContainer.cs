using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Registry;
using Polly.Retry;
using SmartTravel.BookingService.DatabaseContext;
using SmartTravel.BookingService.Helper.Mapping;
using SmartTravel.BookingService.Repositories;
using SmartTravel.BookingService.Services;
using SmartTravel.Shared.Logging;
using SmartTravel.Shared.ServiceRegistration;

namespace SmartTravel.BookingService.Infrastructure
{
    public static class ServiceContainer
    {
        public static IServiceCollection AddInfrastructerServices(this IServiceCollection services, IConfiguration config)
        {
            ServiceRegistration.AddSharedServices<BookingServiceDbContext>(services, config, config["MySerilog: FileName"]!);

            services.AddScoped<IBookingBusinessLayer, BookingBusinessLayer>();
            services.AddScoped<ICarRentalBusinessLayer, CarRentalBusinessLayer>();
            services.AddScoped<IFlightDetailBusinessLayer, FlightDetailBusinessLayer>();
            services.AddScoped<IHotelDetailBusinessLayer, HotelDetailBusinessLayer>();

            services.AddScoped<IBookingRepository, BookingRepository>();
            services.AddScoped<ICarRentalRepository, CarRentalRepository>();
            services.AddScoped<IFlightDetailRepository, FlightDetailRepository>();
            services.AddScoped<IHotelDetailRepository, HotelDetailRepository>();

            services.AddScoped<IBookingMapping, BookingMapping>();
            services.AddScoped<ICarRentalMapping, CarRentalMapping>();
            services.AddScoped<IFlightDetailMapping, FlightDetailMapping>();
            services.AddScoped<IHotelDetailMapping, HotelDetailMapping>();

            services.AddHttpClient<IBookingBusinessLayer, BookingBusinessLayer>(options =>
            {
                options.BaseAddress = new Uri(config["ApiGateWay:BaseAddress"]!);
                options.Timeout = TimeSpan.FromSeconds(1);
            });

            var retryStraegy = new RetryStrategyOptions()
            {
                ShouldHandle = new PredicateBuilder().Handle<TaskCanceledException>(),
                BackoffType = DelayBackoffType.Constant,
                UseJitter = true,
                MaxRetryAttempts = 3,
                Delay = TimeSpan.FromSeconds(500),
                OnRetry = args =>
                {
                    string message = $"OnRetry, Attempt: {args.AttemptNumber} Outcome {args.Outcome}";
                    LoggingExtension.LogToConsole(message);
                    LoggingExtension.LogToDebugger(message);
                    return ValueTask.CompletedTask;
                }
            };

            services.AddResiliencePipeline("my-retry-pipeline", builder =>
            {
                builder.AddRetry(retryStraegy);
            });

            return services;
        }

        public static IApplicationBuilder AddInfrastructurePolicies(this IApplicationBuilder app)
        {
            ServiceRegistration.UseSharedPolicies(app);

            return app;
        }
    }
}
