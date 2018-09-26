using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using UrbanEngine.Core;

namespace UrbanEngine.Infrastructure.Context {
    public class ApplicationDbContext  : DbContext {
        #region Constructors

        private readonly ILoggerFactory _loggerFactory;
        
        public ApplicationDbContext( DbContextOptions<ApplicationDbContext> options, ILoggerFactory loggerFactory ) 
            : base( options ) { 
            _loggerFactory = loggerFactory;
        }

        #endregion

        #region Static/Builder Methods 

        #endregion

        #region Initialization Methods

        protected override void OnConfiguring( DbContextOptionsBuilder optionsBuilder ) {
            // enable logging 
            optionsBuilder
                .UseLoggerFactory( _loggerFactory );
        }

        protected override void OnModelCreating( ModelBuilder modelBuilder ) {
            // let base take care of any initial items 
            base.OnModelCreating( modelBuilder );

            // configure each entity 
            ConfigureEntities( modelBuilder ); 

            // apply any custom conventions 
            // TODO 

        }

        protected virtual void ConfigureEntities( ModelBuilder modelBuilder ) {

            // add User entity 
            modelBuilder.Entity<User>();

        }

        #endregion
    }
}
