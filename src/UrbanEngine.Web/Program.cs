using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using UrbanEngine.Infrastructure.Data;
using Microsoft.Extensions.Configuration;

namespace UrbanEngine.Web
{
	/// <summary>
	/// Core class that is started with the application
	/// </summary>
	public class Program
    {
		/// <summary>
		/// Method that is called to start the application
		/// </summary>
		/// <param name="args"></param>
		public static void Main(string[] args)
        {
            var hostBuilder = CreateHostBuilder(args).Build();

            var logger = hostBuilder.Services.GetRequiredService<ILogger<Program>>();

            try
            {
                logger.LogInformation("Seed Database");
                CreateOrMigrateDatabase<UrbanEngineDbContext>(hostBuilder, logger);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"error trying to call {nameof(CreateOrMigrateDatabase)}");
            }

            logger.LogDebug("Run the application");
            hostBuilder.Run();
        }

		/// <summary>
		/// Method that is called to create and configure a builder object
		/// </summary>
		/// <param name="args"></param>
		/// <returns></returns>
		public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
				.ConfigureAppConfiguration((context, config) => {
					var builtConfig = config.Build();
				
					if (context.HostingEnvironment.IsProduction())
					{
						config.AddAzureKeyVault($"https://{builtConfig["KeyVaultName"]}.vault.azure.net/");
					}
					else
					{
						config.AddUserSecrets<Startup>();
					}
					
				})
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        // this will create database if not exists or update it to latest if it does
        static void CreateOrMigrateDatabase<TContext>(IHost host, ILogger logger) where TContext : DbContext
        {
            var applyMigrations = Environment.GetEnvironmentVariable("APPLY_MIGRATIONS");
            if (applyMigrations != null && applyMigrations.Trim().ToLower() == "true")
            {
                using var scope = host.Services.CreateScope();
                using var context = scope.ServiceProvider.GetService<TContext>();
                logger.LogInformation("attempting to apply migrations");
                context.Database.Migrate();
                logger.LogInformation("migrations applied");
            }
            else
            {
                logger.LogInformation("environment variable APPLY_MIGRATIONS is set to {value} and migrations will NOT be applied", applyMigrations);
            }
        }
    }
}
