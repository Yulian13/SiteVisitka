using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;

namespace SiteVisitka.loggers
{
    public class FileLoggerProvider : ILoggerProvider
    {
        private const string _jsonConfigNamePathLogChangeDatabase = "PathsOfLogs:ChangeDatabase";
        private readonly IConfiguration _configuration;
        private readonly LogsClass _logsClass;

        public FileLoggerProvider(IConfiguration configuration, LogsClass logsClass)
        {
            _configuration = configuration;
            _logsClass = logsClass;
        }

        public ILogger CreateLogger(string categoryName)
        {
            switch (_logsClass)
            {
                case LogsClass.Exeption:
                    return new FileExeptionLogger(_configuration);
                case LogsClass.ChangeDatabase:
                    return null;
            }

            return null;
        }

        public void Dispose()
        {
            
        }
    }
}
