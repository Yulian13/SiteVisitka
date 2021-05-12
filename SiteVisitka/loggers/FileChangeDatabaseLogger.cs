using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.IO;

namespace SiteVisitka.loggers
{
    public class FileChangeDatabaseLogger : ILogger
    {
        private const string _jsonConfigNamePathLogChangeDatabase = "PathsOfLogs:ChangeDatabase";
        private readonly string _exceptionLogPath;
        private static object _lock = new object();

        public FileChangeDatabaseLogger(IConfiguration configuration)
        {
            _exceptionLogPath = Path.Combine(Directory.GetCurrentDirectory(), configuration[_jsonConfigNamePathLogChangeDatabase]);
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
            if (formatter != null & MylogEventId.MyEventIdChangeDB == eventId)
            {
                string message = DateTime.Now.ToString()
                    + Environment.NewLine
                    + formatter(state, exception)
                    + Environment.NewLine;

                lock (_lock)
                {
                    File.AppendAllText(_exceptionLogPath, message);
                }
            }
        }
    }
}
