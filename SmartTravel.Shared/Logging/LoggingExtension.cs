using Serilog;

namespace SmartTravel.Shared.Logging
{
    public static class LoggingExtension
    {
        public static void LogException(Exception ex) 
        { 
            LogToFile(BuildExceptionDetails(ex));
            LogToConsole(ex.Message);
            LogToDebugger(ex.Message);
        }

        public static void LogToFile(string message) => Log.Information(message);
        public static void LogToConsole(string message) => Log.Warning(message);
        public static void LogToDebugger(string message) => Log.Debug(message);

        private static string BuildExceptionDetails(Exception ex)
        {
            var innerExceptionMessage = ex.InnerException?.Message ?? "None";
            return $@"
                Exception Occurred:
                -------------------
                Message: {ex.Message}
                Inner Exception: {innerExceptionMessage}
                Source: {ex.Source}
                Stack Trace: {ex.StackTrace}
                -------------------";
        }
    
    }
}
