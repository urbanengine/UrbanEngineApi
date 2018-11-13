using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using UrbanEngine.Core.Entities;

namespace UrbanEngine.Infrastructure.Context {
    // TODO: review article below and setup creation of migrations 
    // https://docs.microsoft.com/en-us/ef/core/miscellaneous/cli/dbcontext-creation

    public class UrbanEngineContext : DbContext {
        #region Constructors

        private readonly IDbContextSettings _settings;

        private readonly ILoggerFactory _loggerFactory;
        
        public UrbanEngineContext( DbContextOptions<UrbanEngineContext> options )
            : base( options ) { }

        public UrbanEngineContext( DbContextOptions<UrbanEngineContext> options, IOptions<DbContextSettings> settings, ILoggerFactory loggerFactory ) 
            : base( options ) { 
            _loggerFactory = loggerFactory;
            _settings = settings?.Value;
        }

        #endregion
         
        #region Initialization Methods

        protected override void OnConfiguring( DbContextOptionsBuilder optionsBuilder ) {
            // let base take care of initial items 
            base.OnConfiguring( optionsBuilder );

            //// if not previously configured 
            //if( !optionsBuilder.IsConfigured ) {
            //    // enable npgsql 
            //    if( !string.IsNullOrEmpty( _settings?.ConnectionString ) ) {
            //        optionsBuilder.UseNpgsql( _settings?.ConnectionString );
            //    }
            //}

            // enable logging 
            optionsBuilder.UseLoggerFactory( _loggerFactory );          
        }

        protected override void OnModelCreating( ModelBuilder modelBuilder ) {
            // let base take care of any initial items 
            base.OnModelCreating( modelBuilder );

            // apply which schema to use 
            if( !string.IsNullOrEmpty( _settings?.SchemaName ) ) {
                modelBuilder.HasDefaultSchema( _settings.SchemaName );
            }

            // use identity columns
            // make all keys and other properties which have .ValueGeneratedOnAdd() 
            // have Identity by default.
            modelBuilder.ForNpgsqlUseIdentityColumns();

            // apply any custom conventions 
            // TODO 
             
            // configure each entity 
            ConfigureEntities( modelBuilder );

            // seed data 
            SeedData( modelBuilder ); 
        }

        #region Configure Entities 

        protected virtual void ConfigureEntities( ModelBuilder modelBuilder ) {
            #region Conventions (Notes) 

            // By convention, a property named Id or <type name>Id will be configured as the key of an entity.
            // If you want to exclude a property from the model use .Ignore 

            #endregion

            #region Privilege

            modelBuilder.Entity<Privilege>( options => {
                // use identity 
                options.Property( p => p.Id ).ValueGeneratedOnAdd(); 

                // set property values 
                options.Property( p => p.Name ).HasMaxLength( 50 ).IsRequired();
            } );

            #endregion

            #region Role

            modelBuilder.Entity<Role>( options => {
                // use identity 
                options.Property( r => r.Id ).ValueGeneratedOnAdd();

                // set property values 
                options.Property( r => r.Name ).HasMaxLength( 50 ).IsRequired(); 
            } );

            #endregion

            #region RolePrivilege (Join Table)

            modelBuilder.Entity<RolePrivilege>( options => {
                // join table key 
                options.HasKey( rp => new { rp.RoleId, rp.PrivilegeId } );

                // map to role
                options
                 .HasOne( rp => rp.Role )
                 .WithMany( r => r.RolePrivileges )
                 .HasForeignKey( rp => rp.RoleId );

                // map to privilege
                options
                 .HasOne( rp => rp.Privilege )
                 .WithMany( p => p.RolePrivileges )
                 .HasForeignKey( rp => rp.PrivilegeId );
            } );

            #endregion

            #region User

            modelBuilder.Entity<User>( options => {
                // use identity 
                options.Property( u => u.Id ).ValueGeneratedOnAdd();

                // set property values
                options.Property( u => u.AuthZeroId ).HasMaxLength( 50 );
                options.Property( u => u.Bio ).HasMaxLength( 4000 );
                options.Property( u => u.CountryOrRegion ).HasMaxLength( 50 );
                options.Property( u => u.PostalCode ).HasMaxLength( 20 ); 

                // map to company relationship 
                options
                 .HasOne( u => u.Company )
                 .WithMany( c => c.Users )
                 .HasForeignKey( u => u.CompanyId );  
            } );

            #endregion

            #region Tag 

            modelBuilder.Entity<Tag>( options => {
                // use identity 
                options.Property( t => t.Id ).ValueGeneratedOnAdd();

                // set property values 
                options.Property( p => p.Name ).HasMaxLength( 100 ).IsRequired();
                options.Property( p => p.Type ).HasMaxLength( 50 ).IsRequired(); 
            } );

            #endregion

            #region UserRole (Join Table)

            modelBuilder.Entity<UserRole>( options => {
                // join table key 
                options.HasKey( ur => new { ur.UserId, ur.RoleId } );

                // map to user 
                options
                 .HasOne( ur => ur.User )
                 .WithMany( u => u.UserRoles )
                 .HasForeignKey( ur => ur.UserId );

                // map to role 
                options
                 .HasOne( ur => ur.Role )
                 .WithMany( r => r.UserRoles )
                 .HasForeignKey( ur => ur.RoleId );  
            } );

            #endregion

            #region TaggedUser (Join Table)

            modelBuilder.Entity<TaggedUser>( options => {
                // join table key 
                options.HasKey( tu => new { tu.UserId, tu.TagId } );

                // map to user
                options
                 .HasOne( tu => tu.User )
                 .WithMany( u => u.UserTags )
                 .HasForeignKey( tu => tu.UserId );

                // map to tag
                options
                 .HasOne( tu => tu.Tag )
                 .WithMany( t => t.TaggedUsers )
                 .HasForeignKey( tu => tu.TagId ); 
            } );

            #endregion

            #region Company 

            modelBuilder.Entity<Company>( options => {
                // use identity 
                options.Property( c => c.Id ).ValueGeneratedOnAdd();

                // set property values 
                options.Property( c => c.Name ).HasMaxLength( 200 ).IsRequired();
            } );

            #endregion

            #region Venue 

            modelBuilder.Entity<Venue>( options => { 
                // use identity 
                options.Property( v => v.Id ).ValueGeneratedOnAdd();

                // set property values 
                options.Property( v => v.Name ).HasMaxLength( 200 ).IsRequired();
            } );

            #endregion

            #region Event 
            
            modelBuilder.Entity<Event>( options => {
                // use identity 
                options.Property( e => e.Id ).ValueGeneratedOnAdd();

                // set property values 
                options.Property( e => e.Name ).HasMaxLength( 200 ).IsRequired();
                options.Property( e => e.Description ).HasMaxLength( 2000 );

                // map to event
                options
                 .HasOne( e => e.Venue )
                 .WithMany( v => v.Events )
                 .HasForeignKey( e => e.VenueId ); 
            } );

            #endregion
        }

        #endregion

        #region SeedData 

        protected void SeedData( ModelBuilder modelBuilder ) {
             
            // TODO: define all privileges that should be in seed data 
            #region Privilege 

            modelBuilder.Entity<Privilege>().HasData(
                new Privilege { Id = 1, Name = "login" } );

            #endregion

            // TODO: define all roles that should be in seed data 
            #region Role

            modelBuilder.Entity<Role>().HasData(
                new Role { Id = 1, Name = "Site Administrator" },
                new Role { Id = 2, Name = "Events Manager" },
                new Role { Id = 3, Name = "CoWorking Night Workshop Organizer" },
                new Role { Id = 4, Name = "Member" } );

            #endregion

        }

        #endregion

        #endregion
    }
}
