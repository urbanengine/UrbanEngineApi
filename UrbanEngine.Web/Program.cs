using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using UrbanEngine.Infrastructure.Context;

namespace urban_engine_api {
    public class Program {
        public static void Main( string[] args ) {
            BuildWebHost( args ).Run();
        }

        public static IWebHost BuildWebHost( string[] args ) {
            var host = WebHost.CreateDefaultBuilder( args )
                .UseApplicationInsights()
                .UseStartup<Startup>()
                .Build();

            CreateOrMigrateDatabase<UrbanEngineContext>( host ); 

            return host;
        }
         
        // this will create database if not exists or update it to latest if it does 
        static void CreateOrMigrateDatabase<TContext>( IWebHost host ) where TContext : DbContext {
            using( var scope = host.Services.CreateScope() )
            using( var context = scope.ServiceProvider.GetService<TContext>() ) {
                context.Database.Migrate();
            }
        }

    }
}
