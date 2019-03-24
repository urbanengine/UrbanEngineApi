using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.PlatformAbstractions;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.SwaggerGen;
using UrbanEngine.Core.Application.Interfaces.Persistence.Data;
using UrbanEngine.Core.Application.Schedules;
using UrbanEngine.Core.Common.Results;
using UrbanEngine.Infrastructure.Persistence.Data;
using UrbanEngine.Infrastructure.Persistence.Data.Repository;
using UrbanEngine.Web.UrbanEngineApi.Configuration;

namespace UrbanEngine.Web.UrbanEngineApi
{
    public class Startup
    {
        static int _errorEventId = 1; 

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(options => options.EnableEndpointRouting = true)
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
             
            #region Versioning

            services.AddApiVersioning(
                options =>
                {
                    // reporting api versions will return the headers "api-supported-versions" and "api-deprecated-versions"
                    options.ReportApiVersions = true;
                });

            services.AddVersionedApiExplorer(
                options =>
                {
                    // add the versioned api explorer, which also adds IApiVersionDescriptionProvider service
                    // note: the specified format code will format the version as "'v'major[.minor][-status]"
                    options.GroupNameFormat = "'v'VVV";

                    // note: this option is only necessary when versioning by url segment. the SubstitutionFormat
                    // can also be used to control the format of the API version in route templates
                    options.SubstituteApiVersionInUrl = true;
                });

            #endregion

            #region Swagger 

            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
            services.AddSwaggerGen(
                options =>
                {
                    // add a custom operation filter which sets default values
                    options.OperationFilter<SwaggerDefaultValues>();

                    // integrate xml comments
                    options.IncludeXmlComments(XmlCommentsFilePath);
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

            // services
            services.AddTransient<IScheduleService, ScheduleService>();

            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApiVersionDescriptionProvider provider)
        {
            if (!env.IsDevelopment())
            { 
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            #region Error Handling 

            app.UseExceptionHandler(errorApp =>
            {
                errorApp.Run(async context =>
                {
                    // get diagnostic information about the error 
                    var exceptionHandlerPathFeature =
                        context.Features.Get<IExceptionHandlerPathFeature>();

                    // return a 500 status code and generic json message 
                    context.Response.StatusCode = 500;
                    context.Response.ContentType = "application/json";
                     
                    var json = JsonConvert.SerializeObject(new FailureResult(exceptionHandlerPathFeature.Error));
                    await context.Response.WriteAsync(json);

                    // log the error 
                    var logger = errorApp.ApplicationServices.GetService<ILogger<Program>>();
                    logger.LogError(_errorEventId++, exceptionHandlerPathFeature.Error, $"exception caught in UseExceptionHandler middleware, see exception for details");
                });
            });

            #endregion

            #region Swagger

            app.UseSwagger();

            app.UseSwaggerUI(
                options =>
                {
                    // build a swagger endpoint for each discovered API version
                    foreach (var description in provider.ApiVersionDescriptions)
                    {
                        options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
                    }
                });

            #endregion

            app.UseHttpsRedirection();
            app.UseMvc();
        }

        static string XmlCommentsFilePath
        {
            get
            {
                var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                var fileName = typeof(Startup).GetTypeInfo().Assembly.GetName().Name + ".xml";
                return Path.Combine(basePath, fileName);
            }
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
