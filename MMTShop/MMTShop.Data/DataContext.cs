using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Debug;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using MMTShop.Data.Entities.Base;

namespace MMTShop.Data
{
    public class DataContext : DbContext
    {
        public static readonly LoggerFactory loggerFactory = new LoggerFactory(new[] { new DebugLoggerProvider() });
        private readonly IHttpContextAccessor httpContextAccessor;

        public DataContext(DbContextOptions<DataContext> options, IHttpContextAccessor httpContextAccessor) : base(options)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
#if DEBUG
            optionsBuilder.UseLoggerFactory(loggerFactory);
#endif      
            base.OnConfiguring(optionsBuilder);
        }

        public override int SaveChanges()
        {
            AddTimestamps();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            AddTimestamps();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void AddTimestamps()
        {
            // get entries that are being Added or Updated    
            var modifiedEntries = ChangeTracker.Entries()
                .Where(x => x.Entity is Entity && (x.State == EntityState.Added || x.State == EntityState.Modified));

            var userId = httpContextAccessor?.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var currentUsername = !string.IsNullOrEmpty(userId) ? userId : "Anonymous";

            foreach (var entry in modifiedEntries)
            {
                var entity = entry.Entity as Entity;

            }
        }
    }
}
