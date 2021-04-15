using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using MMTShop.Extensions;
using Config = MMTShop.Extensions.ConfigExtensions;
using Logging = MMTShop.Extensions.LoggingExtensions;

namespace MMTShop.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var configuration = Config.CreateDefaultConfigurationBuilder().Build();
            Log.Logger = Logging.CreateSerilogLogger(configuration);
   
            try
            {
                Log.Information("Starting host...");
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly.");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
          Host.CreateDefaultBuilder(args)
          .ConfigureHostConfiguration(config => { })
          .ConfigureAppConfiguration((context, config) => { })
          .ConfigureLogging((context, logging) => { logging.AddSerilog(); })
          .ConfigureServices((context, services) => { services.AddOptions(); })
          .ConfigureWebHostDefaults(webBuilder =>
          {
              webBuilder.UseStartup<Startup>()
              .CaptureStartupErrors(true)
              .ConfigureKestrel(opts => { })
              .UseSerilogLogger<Startup>();
          });
    }
}
