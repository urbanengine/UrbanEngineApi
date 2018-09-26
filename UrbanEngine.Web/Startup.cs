namespace urban_engine_api {
    using System.Reflection;
    using System.IO;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.ApiExplorer;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.PlatformAbstractions;
    using Microsoft.EntityFrameworkCore;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;
    using Swashbuckle.AspNetCore.Swagger;

    using UrbanEngine.Core.Interfaces;
    using UrbanEngine.Web.Configuration;
    using UrbanEngine.Infrastructure.Managers;
    using UrbanEngine.Infrastructure.Repository;
    using UrbanEngine.Infrastructure.Context;
    using Microsoft.Extensions.Logging;

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


            #region Versioning

            // add the versioned api explorer, which also adds IApiVersionDescriptionProvider service
            // note: the specified format code will format the version as "'v'major[.minor][-status]"
            services.AddMvcCore().AddVersionedApiExplorer(
                options => {
                    options.GroupNameFormat = "'v'VVV";

                    // note: this option is only necessary when versioning by url segment. the SubstitutionFormat
                    // can also be used to control the format of the API version in route templates
                    options.SubstituteApiVersionInUrl = true;
                } );

            services.AddApiVersioning( options => {
                // reporting api versions will return the headers "api-supported-versions" and "api-deprecated-versions" 
                options.ReportApiVersions = true;

                // alternative, if we would rather include api-version in the Headers of the request, for now use it in URL
                // options.ApiVersionReader = new HeaderApiVersionReader("api-version"); 
            } );

            #endregion

            #region Swagger 

            // Register the Swagger generator, defining one or more Swagger documents
            services.AddSwaggerGen( options => {  
                // since JS uses camelcase our docs should as well 
                options.DescribeAllParametersInCamelCase(); 

                // resolve the IApiVersionDescriptionProvider service
                // note: that we have to build a temporary service provider here because one has not been created yet
                var provider = services.BuildServiceProvider().GetRequiredService<IApiVersionDescriptionProvider>();

                // add a swagger document for each discovered API version
                // note: you might choose to skip or document deprecated API versions differently
                foreach( var description in provider.ApiVersionDescriptions ) {
                    options.SwaggerDoc( description.GroupName, CreateInfoForApiVersion( description ) );
                }
                 
                // set operation filter to work in conjunction with API Versioning 
                options.OperationFilter<SwaggerDefaultValues>();

                // integrate xml comments
                options.IncludeXmlComments( XmlCommentsFilePath );
            } );

            #endregion

            #region Dependency Injection 

            var connectionString = Configuration.GetConnectionString( "UrbanEngineDbContext" );

            // context   
            services.AddEntityFrameworkNpgsql().AddDbContext<ApplicationDbContext>( 
                options => options.UseNpgsql( connectionString ) 
            );
  
            // repository  
            services.AddScoped<IDbRepository, DbRepository>(); 

            // managers 
            services.AddTransient<IUserManager, UserManager>(); 
             
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure( IApplicationBuilder app, IHostingEnvironment env, IApiVersionDescriptionProvider provider ) {
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
            app.UseSwaggerUI( options => {
                // build a swagger endpoint for each discovered API version
                foreach( var description in provider.ApiVersionDescriptions ) {
                    options.SwaggerEndpoint( $"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant() );
                }
            } );

            #endregion

            app.UseHttpsRedirection(); 
            app.UseMvc(); 
        }

        static string XmlCommentsFilePath {
            get {
                var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                var fileName = typeof( Startup ).GetTypeInfo().Assembly.GetName().Name + ".xml";
                return Path.Combine( basePath, fileName );
            }
        }

        static Info CreateInfoForApiVersion( ApiVersionDescription description ) {
            var info = new Info() {
                Title = $"Urban Engine API {description.ApiVersion}",
                Version = description.ApiVersion.ToString(),
                Description = "UrbanEngine.Web API",
                Contact = new Contact() { Name = "Tyler Hughes", Email = "tyler@urbanengine.org" },
                TermsOfService = "",
                License = new License() { Name = "Apache 2.0", Url = "https://www.apache.org/licenses/LICENSE-2.0.html" }
            };

            if( description.IsDeprecated ) {
                info.Description += " This API version has been deprecated.";
            }

            return info;
        }

        #endregion
    }
}
