using Serilog;

namespace SmartTravel.Shared.Logging
{
    public static class LoggingRegistration
    {
        public static ILogger CreateLogger(string fileName)
        {
            ILogger loggerConfiguration = new LoggerConfiguration()
                .MinimumLevel.Information()
                .WriteTo.Debug()
                .WriteTo.Console()
                .WriteTo.File(path: $"{fileName}-.text",
                    restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Information,
                    outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}",
                    rollingInterval: RollingInterval.Day,
                    retainedFileCountLimit: null,
                    fileSizeLimitBytes: null,
                    rollOnFileSizeLimit: false
                ).CreateLogger();

            return loggerConfiguration;
        }
    }
}
