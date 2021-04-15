using System;
using System.Reflection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
namespace MMTShop.Data
{
    public static class DataExtensions
    {
        /// <summary>
        /// Add Sql Server Database
        /// </summary>
        /// <typeparam name="TContext">DbContext</typeparam>
        /// <param name="services">Service Collection</param>
        /// <param name="name">Connection Name</param>  
        public static void AddSqlServerDatabase<TContext>(this IServiceCollection services, string name) where TContext : DbContext
        {
            var serviceProvider = services.BuildServiceProvider();
            var configuration = serviceProvider.GetService<IConfiguration>();
            var hostingEnvironment = serviceProvider.GetService<IWebHostEnvironment>();

            var migrationsAssemblyName = hostingEnvironment == null
                ? Assembly.GetEntryAssembly().GetName().Name
                : hostingEnvironment.ApplicationName;

            AddSqlServerDatabase<TContext>(services, configuration, name, migrationsAssemblyName);
        }

        /// <summary>
        /// Add Sql Server Database
        /// </summary>
        /// <typeparam name="TContext">DbContext</typeparam>
        /// <param name="configuration">Configuration</param>
        /// <param name="services">Service Collection</param>
        /// <param name="name">Connection Name</param>
        /// <param name="migrationsAssemblyName">Configures the assembly where migrations are maintained for this context.</param>
        public static void AddSqlServerDatabase<TContext>(this IServiceCollection services, IConfiguration configuration, string name, string migrationsAssemblyName) where TContext : DbContext
        {
            var connectionString = configuration.GetConnectionString(name);

            services.AddDbContext<TContext>(options =>
            options
              .EnableSensitiveDataLogging(true)
              .UseSqlServer(connectionString,
              sqlServerOptionsAction: sqlOptions =>
              {
                  sqlOptions.CommandTimeout(300);
                  sqlOptions.MigrationsAssembly(migrationsAssemblyName);
                  sqlOptions.EnableRetryOnFailure(maxRetryCount: 5, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                  sqlOptions.UseNetTopologySuite();
              }));
        }
    }
}
