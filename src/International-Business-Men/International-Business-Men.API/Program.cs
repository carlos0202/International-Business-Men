using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Formatting.Compact;

namespace International_Business_Men.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // default logger to catch nasty startup errors.
            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.File(new RenderedCompactJsonFormatter(), "/logs/log-global.json")
                .CreateLogger();
            try
            {
                Log.Information("Starting up");
                CreateHostBuilder(args).Build().Run();
            } catch(Exception ex)
            {
                Log.Fatal(ex, "Application start-up failed");
            } finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
