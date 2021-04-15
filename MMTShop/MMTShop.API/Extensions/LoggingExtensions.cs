using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Core;

namespace MMTShop.Extensions
{
    /// <summary>
    /// Logging Extensions
    /// </summary>
    public static class LoggingExtensions
    {
        /// <summary>
        /// Create Serilog Logger.
        /// </summary>
        /// <param name="configuration">IConfiguration</param>
        /// <returns>Logger</returns>
        public static Logger CreateSerilogLogger(IConfiguration configuration)
        {
            var logger = new LoggerConfiguration()
              //.WriteTo.Console()
              .Enrich.FromLogContext()
              .ReadFrom.Configuration(configuration)
              .CreateLogger();
            return logger;
        }

        /// <summary>
        /// Use Serilog Logger.
        /// </summary>
        /// <typeparam name="TStartup">Startup</typeparam>
        /// <param name="builder">IWebHostBuilder</param>
        /// <returns>IWebHostBuilder</returns>
        public static IWebHostBuilder UseSerilogLogger<TStartup>(this IWebHostBuilder builder) where TStartup : class
        {
            builder.UseSerilog((hostingContext, loggerConfiguration) =>
            {
                loggerConfiguration
                .ReadFrom.Configuration(hostingContext.Configuration)
                .Enrich.FromLogContext()
                .Enrich.WithProperty("Application", typeof(TStartup).Assembly.GetName().Name)
                .Enrich.WithProperty("Environment", hostingContext.HostingEnvironment.EnvironmentName);
            });
            return builder;
        }
    }
}

