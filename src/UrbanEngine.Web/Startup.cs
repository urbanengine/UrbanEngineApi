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
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace UrbanEngine.Web
{
	/// <summary>
	/// This class configures services and the app's request pipeline.
	/// </summary>
	public class Startup
    {
        static int _errorEventId = 1;

		/// <summary>
		/// Constructor method for the Startup class
		/// </summary>
		/// <param name="configuration"></param>
		public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

		/// <summary>
		/// Property representing the applications configuration
		/// </summary>
		public IConfiguration Configuration { get; }

		/// <summary>
		/// This method gets called by the runtime. Use this method to add services to the container.
		/// </summary>
		/// <param name="services"></param>
		public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

			#region Authentication

			services.AddAuthentication( options =>
			{
				options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

			} )
			 .AddJwtBearer( options =>
			 {
				 options.Audience = Configuration[ "Auth0:ApiIdentifier" ];
				 options.Authority = $"https://{Configuration[ "Auth0:Domain" ]}/";
			 } );

			#endregion

			#region Swagger

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

			#endregion

			#region Database Provider Setup

			// db context
			services.AddDbContext<UrbanEngineDbContext>(options =>
            {
                //options.UseSqlite("Data Source=UrbanEngine.db");
				options.EnableSensitiveDataLogging();
                options.UseNpgsql("host=localhost;database=postgres_local;user id=postgres_admin;password=Postgres2020!;");
                options.UseLoggerFactory(GetLoggerFactory());
            });

			#endregion

			#region Dependency Injection

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

			#endregion
		}

		/// <summary>
		/// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		/// </summary>
		/// <param name="app"></param>
		/// <param name="env"></param>
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
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

			app.UseAuthentication();

			app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Urban Engine API v1");
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
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
    }
}
