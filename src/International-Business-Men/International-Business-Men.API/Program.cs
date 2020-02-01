using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace International_Business_Men.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // default logger to catch nasty startup errors.
            //TODO Configure log to catch fatal error during app boot.
            try
            {
                CreateHostBuilder(args).Build().Run();
            } catch(Exception)
            {
            } finally
            {
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureLogging(logging =>
                {
                    logging.ClearProviders();
                    logging.AddConsole();
                    logging.AddDebug();
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
