using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SiteVisitka.loggers;
using SiteVisitka.Models.SQL_models.Works;
using System;

namespace SiteVisitka.Serviсes
{
    public class MyFileLogger
    {
        private readonly ILogger _logger;
        public MyFileLogger(IConfiguration configuration, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddLoggerException(configuration);
            loggerFactory.AddLoggerChangeDatabase(configuration);
            _logger = loggerFactory.CreateLogger("FileLogger");
        }

        public void LogException(Exception ex)
        {
            _logger.LogError(MylogEventId.MyEventIdException, ex, "MyLogException");
        }

        public void logAddWorkToDB(Work work)
        {
            string message = $"A work with the id {work.Id} has been added.";
            _logger.LogInformation(MylogEventId.MyEventIdChangeDB, message);
        }

        public void logPostSqlRequest(string request)
        {
            string message = "Request was sent"
                + Environment.NewLine
                + request;
            _logger.LogInformation(MylogEventId.MyEventIdChangeDB, message);
        }
    }
}
