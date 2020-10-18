using System;
using System.IO;
using System.Reflection;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using UrbanEngine.Core.Entities;
using UrbanEngine.Core.Managers.CheckIn;
using UrbanEngine.Core.Managers.Events;
using UrbanEngine.Core.Managers.Venues;
using UrbanEngine.Infrastructure.Data;
using UrbanEngine.Infrastructure.Data.Repository;
using UrbanEngine.SharedKernel.Data;
using UrbanEngine.SharedKernel.Results;
using UrbanEngine.Core.Handlers.Venues;
using UrbanEngine.Core.Managers.Rooms;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNet.OData.Formatter;
using Microsoft.Net.Http.Headers;
using System.Linq;
using Microsoft.OData.Edm;

namespace UrbanEngine.Web
{
	/// <summary>
	/// startup
	/// </summary>
    public class Startup
    {
        static int _errorEventId = 1;

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
			//services.AddApiVersioning(options =>
			//{
			//	options.DefaultApiVersion = new ApiVersion(1, 0);
			//	options.AssumeDefaultVersionWhenUnspecified = true;
			//});

			services.AddOData();//.EnableApiVersioning();
			services.AddControllers();
			
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
			
			// Swagger Config
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Urban Engine API",
                    Version = "v1",
                    Description = "Urban Engine API",
                    // TermsOfService = new Uri(""),
                    Contact = new OpenApiContact
                    {
                        Name = "Tyler Hughes",
                        Email = "tyler@urbanengine.org"
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Apache 2.0",
                        Url = new Uri("https://www.apache.org/licenses/LICENSE-2.0.html")
                    }
                });

				// Set the comments path for the Swagger JSON and UI.
				var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
				var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
				c.IncludeXmlComments(xmlPath);
			});

			// output formatters
            SetOutputFormatters(services);

        }

		/// <summary>
		/// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		/// </summary>
		/// <param name="app"></param>
		/// <param name="env"></param>
		/// <param name="modelBuilder"></param>
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env) //, VersionedODataModelBuilder modelBuilder)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
			else
			{
				app.UseHsts();
			}

            app.UseExceptionHandler(errorApp =>
            {
                errorApp.Run(async context =>
                {
                    // get diagnostic information about the error
                    var exceptionHandlerPathFeature =
                        context.Features.Get<IExceptionHandlerPathFeature>();

                    // get the exception that was thrown from endpoint
                    var exceptionThrown = exceptionHandlerPathFeature.Error;

                    // return a status code and generic json message
                    context.Response.StatusCode = FailureResult.GetStatusCode(exceptionThrown);
                    context.Response.ContentType = "application/json";

                    var json = JsonConvert.SerializeObject(new FailureResult(exceptionThrown));
                    await context.Response.WriteAsync(json);

                    // log the error
                    var logger = errorApp.ApplicationServices.GetService<ILogger<Program>>();
                    logger.LogError(_errorEventId++, exceptionThrown, $"exception caught in UseExceptionHandler middleware, see exception for details");
                });
            });

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
                endpoints.EnableDependencyInjection();
                endpoints.Select().Filter().Expand().MaxTop(100);
                endpoints.MapODataRoute("odata", "odata", GetEdmModel());
			});
			
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Urban Engine API v1");
            });
        }

		private IEdmModel GetEdmModel()
		{
			var builder = new ODataConventionModelBuilder();
            
			builder.EntitySet<CheckInEntity>("CheckInEntity");
			builder.EntitySet<EventEntity>("EventEntity");
			builder.EntitySet<EventVenueEntity>("EventVenueEntity");
			builder.EntitySet<RoomEntity>("RoomEntity");

            return builder.GetEdmModel();
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
		private static void SetOutputFormatters(IServiceCollection services)
        {
            services.AddMvcCore(options =>
            {
                IEnumerable<ODataOutputFormatter> outputFormatters =
                    options.OutputFormatters.OfType<ODataOutputFormatter>()
                        .Where(foramtter => foramtter.SupportedMediaTypes.Count == 0);

                foreach (var outputFormatter in outputFormatters)
                {
                    outputFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/odata"));
                }
            });
        }
    }
}
