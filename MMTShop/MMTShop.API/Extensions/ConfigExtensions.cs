using System;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace MMTShop.Extensions
{
    /// <summary>
    /// Configuration Extensions
    /// </summary>
    public static class ConfigExtensions
    {
        private const string EnvironmentVariable = "ASPNETCORE_ENVIRONMENT";

        /// <summary>
        /// Create Default Configuration Builder
        /// </summary>
        /// <returns>IConfigurationBuilder</returns>
        public static IConfigurationBuilder CreateDefaultConfigurationBuilder()
        {
            return CreateDefaultConfigurationBuilder(Directory.GetCurrentDirectory(), Environment.GetEnvironmentVariable(EnvironmentVariable));
        }

        /// <summary>
        /// Create Default Configuration Builder
        /// </summary>
        /// <param name="basePath">Base Path</param>
        /// <param name="environmentName">Environment Name</param>
        /// <returns>IConfigurationBuilder</returns>
        public static IConfigurationBuilder CreateDefaultConfigurationBuilder(string basePath, string environmentName)
        {
            var builder = new ConfigurationBuilder()
           .SetBasePath(basePath)
           .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
           .AddJsonFile($"appsettings.{environmentName ?? "Production"}.json", optional: true)
           .AddEnvironmentVariables();
            return builder;
        }
    }
}

