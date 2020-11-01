using System;
using System.IO;
using System.Reflection;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using UrbanEngine.Core.Entities;
using UrbanEngine.Core.Managers.CheckIn;
using UrbanEngine.Core.Managers.Events;
using UrbanEngine.Core.Managers.Venues;
using UrbanEngine.Infrastructure.Data;
using UrbanEngine.Infrastructure.Data.Repository;
using UrbanEngine.SharedKernel.Data;
using UrbanEngine.Core.Handlers.Venues;
using UrbanEngine.Core.Managers.Rooms;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNet.OData.Builder;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.AspNetCore.Http;

namespace UrbanEngine.Web
{
	/// <summary>
	/// startup
	/// </summary>
	public class Startup
    {
		/// <summary>
		/// startup
		/// </summary>
		/// <param name="configuration"></param>
		public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

		/// <summary>
		/// configuration
		/// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
		/// This method gets called by the runtime. Use this method to add services to the container.
		/// </summary>
		/// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
			services.AddControllers();
            services.AddApiVersioning( options => options.ReportApiVersions = true );
            services.AddOData().EnableApiVersioning();
            services.AddODataApiExplorer(
                options =>
                {
                    // add the versioned api explorer, which also adds IApiVersionDescriptionProvider service
                    // note: the specified format code will format the version as "'v'major[.minor][-status]"
                    options.GroupNameFormat = "'v'VVV";

                    // note: this option is only necessary when versioning by url segment. the SubstitutionFormat
                    // can also be used to control the format of the API version in route templates
                    options.SubstituteApiVersionInUrl = true;
                } ); 
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
            services.AddSwaggerGen(
                options =>
                {
                    // add a custom operation filter which sets default values
                    options.OperationFilter<SwaggerDefaultValues>();

                    // integrate xml comments
                    options.IncludeXmlComments( XmlCommentsFilePath );
                } );

            // db context
            services.AddDbContext<UrbanEngineDbContext>(options =>
            {
				var dbConnString = Configuration.GetValue<string>("UrbanEngine:Database");
				if(string.IsNullOrWhiteSpace(dbConnString))
					throw new InvalidOperationException("Unable to find configuration value for database connection string.");

                //options.UseSqlite("Data Source=UrbanEngine.db");
				options.EnableSensitiveDataLogging();
                options.UseNpgsql(dbConnString);
                options.UseLoggerFactory(GetLoggerFactory());
            });

            // repositories
            services.AddScoped<IAsyncRepository<EventEntity>, EventRepository>();
            services.AddScoped<IAsyncRepository<EventVenueEntity>, EventVenueRepository>();
            services.AddScoped<IAsyncRepository<CheckInEntity>, CheckInRepository>();
            services.AddScoped<IAsyncRepository<RoomEntity>, RoomRepository>();

            // managers
            services.AddScoped<IEventManager, EventManager>();
            services.AddScoped<IEventVenueManager, EventVenueManager>();
            services.AddScoped<ICheckInManager, CheckInManager>();
            services.AddScoped<IRoomManager, RoomManager>();

            // AutoMapper
            services.AddAutoMapper(typeof(Configuration.AutoMapperProfile).Assembly);

            // Mediatr
            services.AddMediatR(typeof(GetVenuesHandler).Assembly);
        }

		/// <summary>
		/// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		/// </summary>
		/// <param name="app"></param>
		/// <param name="modelBuilder"></param>
		/// <param name="provider"></param>
		public void Configure(IApplicationBuilder app, VersionedODataModelBuilder modelBuilder, IApiVersionDescriptionProvider provider )
        {
            app.UseRouting();
            app.UseAuthorization();
			app.UseHttpsRedirection();

			app.UseEndpoints(endpoints =>
			{
                endpoints.Count();
                endpoints.MapVersionedODataRoute( "odata", "api", modelBuilder );
			});

			app.UseSwagger();
            app.UseSwaggerUI(
                options =>
                {
                    // build a swagger endpoint for each discovered API version
                    foreach ( var description in provider.ApiVersionDescriptions )
                    {
                        options.SwaggerEndpoint( $"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant() );
                    }
                } );
        }

        private ILoggerFactory GetLoggerFactory()
        {
            IServiceCollection serviceCollection = new ServiceCollection();
            serviceCollection.AddLogging(builder =>
                   builder.AddConsole()
                          .AddFilter(DbLoggerCategory.Database.Command.Name,
                                     LogLevel.Information));
            return serviceCollection.BuildServiceProvider()
                    .GetService<ILoggerFactory>();
        }

		static string GetCorrelationId(HttpRequest request)
		{
			var headerKey = "";

			if(request.Headers.ContainsKey("traceparent"))
				headerKey = "traceparent";
			else if(request.Headers.ContainsKey("X-Correlation-ID"))
				headerKey = "X-Correlation-ID";

			return !string.IsNullOrEmpty(headerKey) 
				? request.Headers[headerKey].ToString()
				: Guid.NewGuid().ToString();
		}

		static string XmlCommentsFilePath
        {
            get
            {
                var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                var fileName = typeof( Startup ).GetTypeInfo().Assembly.GetName().Name + ".xml";
                return Path.Combine( basePath, fileName );
            }
        }
    }
}
