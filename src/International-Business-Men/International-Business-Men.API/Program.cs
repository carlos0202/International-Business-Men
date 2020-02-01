using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
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
                .WriteTo.File(new RenderedCompactJsonFormatter(), "/logs/global-log.json")
                .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
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
