using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.Infrastructure.Persistence
{
    public static class MigrateAndSeedDb
    {
        public static async Task<IHost> MigrateDatabaseAsync(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var logger = services.GetRequiredService<ILogger<SalesContextSeed>>();
                var context = services.GetService<SalesDbContext>();

                try
                {
                    logger.LogInformation("Migrating database associated with context {DbContextName}", typeof(SalesDbContext).Name);


                    context.Database.Migrate();
                    await SalesContextSeed.SeedAsync(context, logger);

                    logger.LogInformation("Migrated database associated with context {DbContextName}", typeof(SalesDbContext).Name);
                }
                catch (SqlException ex)
                {
                    logger.LogError(ex, "An error occurred while migrating the database used on context {DbContextName}", typeof(SalesDbContext).Name);
                }
            }

            return host;
        }
    }
}
