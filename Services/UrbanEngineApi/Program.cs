using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using UrbanEngine.Infrastructure.Persistence.Data;

namespace UrbanEngine.Services.UrbanEngineApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();

            var logger = host.Services.GetRequiredService<ILogger<Program>>();

            try
            {
                logger.LogInformation("Seed Database");
                CreateOrMigrateDatabase<UrbanEngineDbContext>(host, logger);
            }
            catch(Exception ex)
            {
                logger.LogError(ex, $"error trying to call {nameof(CreateOrMigrateDatabase)}");
            }

            logger.LogDebug("Run the application"); 
            host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();

        // this will create database if not exists or update it to latest if it does 
        static void CreateOrMigrateDatabase<TContext>(IWebHost host, ILogger logger) where TContext : DbContext
        {
            var applyMigrations = Environment.GetEnvironmentVariable("APPLY_MIGRATIONS");
            if (applyMigrations.Trim().ToLower() == "true")
            {
                using (var scope = host.Services.CreateScope())
                using (var context = scope.ServiceProvider.GetService<TContext>())
                {
                    logger.LogInformation("attempting to apply migrations");
                    context.Database.Migrate();
                    logger.LogInformation("migrations applied");
                }
            }
            else
            {
                logger.LogInformation("environment variable APPLY_MIGRATIONS is set to {value} and migrations will NOT be applied", applyMigrations);
            }
        }
    }
}
