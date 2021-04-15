using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace MMTShop.Data
{
    public class DataContextFactory : IDesignTimeDbContextFactory<DataContext>
    {
        public DataContext CreateDbContext(string[] args)
        {
            // Get environment
            //string environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            if (args != null && args.Length > 0)
            {
                var config = new ConfigurationBuilder()
                .SetBasePath(args[0])
                //.SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                 //.AddJsonFile($"appsettings.{environment}.json", optional: true)
                 .AddJsonFile($"appsettings.Development.json", optional: true)
                .AddEnvironmentVariables()
                .Build();

                // Get connection string
                var optionsBuilder = new DbContextOptionsBuilder<DataContext>();
                var connectionString = config.GetConnectionString("DefaultConnection");
                optionsBuilder.UseSqlServer(connectionString, options =>
                {
                    options.UseNetTopologySuite();
                });

                return new DataContext(optionsBuilder.Options, null);
            }
            else
            {
                // Build config
                var config = new ConfigurationBuilder()
                    .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../MMTShop.API"))
                    //.SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                     //.AddJsonFile($"appsettings.{environment}.json", optional: true)
                     .AddJsonFile($"appsettings.Development.json", optional: true)
                    .AddEnvironmentVariables()
                    .Build();

                // Get connection string
                var optionsBuilder = new DbContextOptionsBuilder<DataContext>();
                var connectionString = config.GetConnectionString("DefaultConnection");
                optionsBuilder.UseSqlServer(connectionString, options =>
                 {
                     options.UseNetTopologySuite();
                 });

                return new DataContext(optionsBuilder.Options, null);
            }
        }
    }
}
