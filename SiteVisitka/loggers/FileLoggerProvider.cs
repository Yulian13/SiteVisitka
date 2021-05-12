using Microsoft.Extensions.Logging;

namespace SiteVisitka.loggers
{
    public class FileLoggerProvider : ILoggerProvider
    {
        private readonly ILogger _logger;

        public FileLoggerProvider(ILogger logger)
        {
            _logger = logger;
        }

        public ILogger CreateLogger(string categoryName) => _logger;

        public void Dispose()
        {
            
        }
    }
}
