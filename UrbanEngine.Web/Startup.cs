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
using Swashbuckle.AspNetCore.Swagger;
using System.Reflection;
using System.IO;
using System;

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

            #region Swagger 
             
            // Register the Swagger generator, defining one or more Swagger documents
            services.AddSwaggerGen( c => {
                c.SwaggerDoc( "v1", new Info {
                    Version = "v1",
                    Title = "UrbanEngine.Web",
                    Description = "Urban Engine API"
                    // TODO add all of these details to match Urban Engine information 
                    //, Contact = new Contact { }
                    //, License = new License { }
                    //, TermsOfService = ""
                } );

                // Set the comments path for the Swagger JSON and UI.
                string[] xmlFiles = new string[] {
                    // include xml for this assmebly 
                    $"{Assembly.GetEntryAssembly().GetName().Name}.xml",
                    // include other assemblys
                    "UrbanEngine.Core.xml" 
                };

                foreach( var xmlFile in xmlFiles ) {
                    var xmlPath = Path.Combine( AppContext.BaseDirectory, xmlFile );
                    c.IncludeXmlComments( xmlPath );
                }

                // since JS uses camelcase our docs should as well 
                c.DescribeAllParametersInCamelCase();
            } );

            #endregion

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

            #region Swagger

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
            app.UseSwaggerUI( c => {
                c.SwaggerEndpoint( "/swagger/v1/swagger.json", "UrbanEngine.Web V1" );
            } );

            #endregion

            app.UseHttpsRedirection(); 
            app.UseMvc(); 
        }

        #endregion
    }
}
