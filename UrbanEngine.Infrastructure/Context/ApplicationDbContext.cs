using Microsoft.EntityFrameworkCore; 
using UrbanEngine.Core;

namespace UrbanEngine.Infrastructure.Context {
    public class ApplicationDbContext  : DbContext {
        #region Constructors
        
        public ApplicationDbContext( DbContextOptions options ) 
            : base( options ) { }

        #endregion

        #region Static/Builder Methods 

        #endregion

        #region Initialization Methods

        protected override void OnConfiguring( DbContextOptionsBuilder optionsBuilder ) { }

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
