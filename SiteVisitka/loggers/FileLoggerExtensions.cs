using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace SiteVisitka.loggers
{
    public static class FileLoggerExtensions
    {
        public static ILoggerFactory AddLoggerException(this ILoggerFactory factory,
                                        IConfiguration configuration)
        {
            factory.AddProvider(new FileLoggerProvider(configuration, LogsClass.Exeption));
            return factory;
        }

        public static ILoggerFactory AddLoggerChangeDatabase(this ILoggerFactory factory,
                                       IConfiguration configuration)
        {
            factory.AddProvider(new FileLoggerProvider(configuration, LogsClass.ChangeDatabase));
            return factory;
        }
    }
}
