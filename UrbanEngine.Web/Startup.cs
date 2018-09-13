using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting; 
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UrbanEngine.Core.Interfaces;
using UrbanEngine.Infrastructure.Managers;
using UrbanEngine.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using UrbanEngine.Infrastructure.Context;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace urban_engine_api {
    public class Startup {
        #region Properties

        public IConfiguration Configuration { get; }

        #endregion

        #region Constructor

        public Startup( IConfiguration configuration ) {
            Configuration = configuration;
        }

        #endregion

        #region Public Methods

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices( IServiceCollection services ) {
            services.AddMvc()
                .SetCompatibilityVersion( CompatibilityVersion.Version_2_1 )
                .AddJsonOptions( options => {
                    // ignore self referencing loops
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                    // force camel case of JSON 
                    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                } );

            #region Dependency Injection 

            // context   
            services.AddDbContext<ApplicationDbContext>( options =>
                options.UseInMemoryDatabase( "InMemoryDb" ) // TODO, switch over to actual db implementation 
            );

            // repository  
            services.AddScoped<IDbRepository, DbRepository>(); 

            // managers 
            services.AddTransient<IUserManager, UserManager>(); 
             
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure( IApplicationBuilder app, IHostingEnvironment env ) {
            // to disable AppInsights Telemetry uncomment next two lines 
            // var configuration = app.ApplicationServices.GetService<Microsoft.ApplicationInsights.Extensibility.TelemetryConfiguration>();
            // configuration.DisableTelemetry = true;

            if( env.IsDevelopment() ) {
                app.UseDeveloperExceptionPage();
            } else {
                app.UseHsts();
            }  

            app.UseHttpsRedirection(); 
            app.UseMvc(); 
        }

        #endregion
    }
}
