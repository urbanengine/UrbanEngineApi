using System;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using UrbanEngine.Core.Application.Events;
using UrbanEngine.Core.Application.Interfaces.Persistence.Data;
using UrbanEngine.Core.Application.Schedules;
using UrbanEngine.Core.Application.Venues;
using UrbanEngine.Core.Common.Results;
using UrbanEngine.Infrastructure.Persistence.Data;
using UrbanEngine.Infrastructure.Persistence.Data.Repository;
using Microsoft.OpenApi.Models;

namespace UrbanEngine.Services.UrbanEngineApi
{
    public class Startup
    {
        static int _errorEventId = 1;

        /// <summary>
        /// Constructor for UrbanEngineApi Startup class
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// property for the application's configuration settings
        /// </summary>
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

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

            #region Dependency Injection 

            // db context 
            services.AddDbContext<UrbanEngineDbContext>(options =>
            {
                options.UseSqlite("Data Source=UrbanEngine.db");
                options.UseLoggerFactory(GetLoggerFactory());
            });

            // repositories  
            services.AddScoped<IEventRepository, EventRepository>();
            services.AddScoped<IEventVenueRepository, EventVenueRepository>();

            // services
            services.AddTransient<IScheduleService, ScheduleService>();
            services.AddTransient<IEventVenueService, EventVenueService>();
            services.AddTransient<IEventService, EventService>();

            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            #region Error Handling 

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

            #endregion

            #region Swagger

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Urban Engine API v1");
                //c.RoutePrefix = string.Empty;
            });

            #endregion

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
