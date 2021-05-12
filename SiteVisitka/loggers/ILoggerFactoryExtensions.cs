using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace SiteVisitka.loggers
{
    public static class ILoggerFactoryExtensions
    {
        public static ILoggerFactory AddLoggerException(this ILoggerFactory factory,
                                        IConfiguration configuration)
        {
            factory.AddProvider(new FileLoggerProvider
                (
                    new FileExeptionLogger(configuration)
                ));
            return factory;
        }

        public static ILoggerFactory AddLoggerChangeDatabase(this ILoggerFactory factory,
                                       IConfiguration configuration)
        {
            factory.AddProvider(new FileLoggerProvider
                (
                    new FileChangeDatabaseLogger(configuration)
                ));
            return factory;
        }
    }
}
