using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.IO;

namespace SiteVisitka.loggers
{
    public class FileExeptionLogger : ILogger
    {
        private const string _jsonConfigNamePathLogException = "PathsOfLogs:Exceptions";

        private readonly string _exceptionLogPath;
        private static object _lock = new object();

        public FileExeptionLogger(IConfiguration configuration)
        {
            _exceptionLogPath = Path.Combine(Directory.GetCurrentDirectory(), configuration[_jsonConfigNamePathLogException]);
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (formatter != null & exception != null & MylogEventId.MyEventIdException == eventId)
            {
                string message = DateTime.Now.ToString()
                    + Environment.NewLine
                    + exception.Message
                    + Environment.NewLine;

                lock (_lock)
                {
                    File.AppendAllText(_exceptionLogPath, message);
                }
            }
        }
    }
}
