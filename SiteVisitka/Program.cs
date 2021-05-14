using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;

namespace SiteVisitka
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                string path = Path.Combine(Directory.GetCurrentDirectory(), "logs\\FailCreateHostBuilder.txt");
                string message = DateTime.Now.ToString()
                    + Environment.NewLine
                    + ex.Message
                    + Environment.NewLine;

                File.AppendAllText(path, message);
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    webBuilder.ConfigureAppConfiguration(configure => {
                        configure.AddJsonFile("URLsettings.json");
                    });
                });
    }

}
