using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using UrbanEngine.Core.Entities;

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
            #region Conventions (Notes) 

            // By convention, a property named Id or <type name>Id will be configured as the key of an entity.

            #endregion

            #region Privilege

            modelBuilder.Entity<Privilege>( options => {
                // set property values 
                options.Property( p => p.Name ).HasMaxLength( 50 );
            } );

            #endregion

            #region Role

            modelBuilder.Entity<Role>( options => {
                // set property values 
                options.Property( r => r.Name ).HasMaxLength( 50 );

                // map to role 

            } );

            #endregion

            #region User

            modelBuilder.Entity<User>( options => {   
                // map to company relationship 
                options
                 .HasOne( u => u.Company )
                 .WithMany( c => c.Users )
                 .HasForeignKey( u => u.CompanyId );  
            } );

            #endregion

            #region UserRole 

            modelBuilder.Entity<UserRole>( options => {
                // join table key 
                options.HasKey( ur => new { ur.UserId, ur.RoleId } );

                // map to user 
                options
                 .HasOne( ur => ur.User )
                 .WithMany( u => u.Roles )
                 .HasForeignKey( ur => ur.UserId );

                // map to role 
                options
                 .HasOne( ur => ur.Role )
                 .WithMany( r => r.Users )
                 .HasForeignKey( ur => ur.RoleId );  
            } );

            #endregion

            #region TaggedUser 

            #endregion

            #region Company 

            modelBuilder.Entity<Company>( options => {
                // set property values 
                options.Property( c => c.Name ).HasMaxLength( 200 );
            } );

            #endregion

            #region Venue 

            modelBuilder.Entity<Venue>( options => {
                // set property values 
                options.Property( v => v.Name ).HasMaxLength( 200 );
            } );

            #endregion

            #region Event 
            
            modelBuilder.Entity<Event>( options => {
                // set property values 
                options.Property( e => e.Name ).HasMaxLength( 200 );

            } );

            #endregion

            #region Tag 

            modelBuilder.Entity<Tag>( options => {
                // set property values 
                options.Property( p => p.Name ).HasMaxLength( 100 );
            } );

            #endregion
        }

        #endregion
    }
}
