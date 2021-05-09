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
            if (formatter != null)
            {
                lock (_lock)
                {
                    File.AppendAllText(_exceptionLogPath, formatter(state, exception) + Environment.NewLine);
                }
            }
        }

        //        catch (Exception ex)
        //        {
        //            string path = Path.Combine(Directory.GetCurrentDirectory(), "/Logs/MainException.txt");
        //string message = $"{DateTime.Now} \r\t {ex.Message}";

        //File.WriteAllText(path, message);
        //        }



    }
}
