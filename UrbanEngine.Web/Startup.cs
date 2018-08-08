using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UrbanEngine.Core.Interfaces;
using UrbanEngine.Infrastructure.Managers;
using UrbanEngine.Infrastructure.Repository;

namespace urban_engine_api
{
    public class Startup
    {
        #region Properties

        public IConfiguration Configuration { get; }

        #endregion

        #region Constructor

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        #endregion

        #region Public Methods

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddAutofac();

            RegisterControllers(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }

        // Use this method to register things directly with Autofac
        public void ConfigureContainer(ContainerBuilder builder)
        {
            RegisterManagers(builder);
        }

        #endregion

        #region Private Methods

        private void RegisterControllers(IServiceCollection services)
        {
            services.AddTransient<IUserManager, UserManager>();
        }

        private void RegisterManagers(ContainerBuilder builder)
        {
            builder.RegisterType<IUserManager>().As<UserManager>();
            builder.RegisterType<IDbRepository>().As<DbRepository>();
        }

        #endregion
    }
}
