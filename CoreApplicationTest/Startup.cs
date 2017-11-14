using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using CoreApplicationTest.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using System.IO;
using Microsoft.EntityFrameworkCore.Design;
using CoreApplication.Data.Repositories;
using CoreApplication.Data.Repositories.Interfaces;
using CoreApplication.Data;
using CoreApplication.Data.Uow;

namespace CoreApplicationTest
{
    public class Startup
    {
        private IHostingEnvironment _env;
       // private IConfigurationRoot _config;
        

        private  IConfiguration _config { get; }
        public Startup(IConfiguration config,IHostingEnvironment env)
        {
            _env = env;
            //var builder = new ConfigurationBuilder()
            //    .SetBasePath(_env.ContentRootPath)
            //    .AddJsonFile("config.json")
            //    .AddEnvironmentVariables();

            _config = config;
           // Configuration = config;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            if (_env.IsEnvironment("Development") || _env.IsEnvironment("Testing"))
            {
                services.AddScoped<IMailService, DebugMailService>();
            }

            //else implement a production one for the mail service here
            services.AddMvc();
            services.AddSingleton(_config);

            services.AddDbContext<CoreContext>(options =>
            options.UseSqlServer(_config.GetConnectionString("CoreContextConnection")));

            services.AddTransient<IUnitOfWork, UnitOfWork>();

            services.AddTransient(typeof(ICoreRepository<>), typeof(CoreRepository<>));
            services.AddTransient<ContextSeedData>();
           

            //services.AddTransient<ContextSeedData>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app,
            IHostingEnvironment env)
         //   ContextSeedData seeder)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            app.UseMvc(config =>
            {
                config.MapRoute(
                    name: "Default",
                    template: "{controller}/{action}/{id?}",
                        defaults: new { controller = "App", action = "Index" }
                );
            });

          //  seeder.EnsureDataSeed().Wait();

            if(_env.IsDevelopment())
            {
                //Seed the database
                using (var scope = app.ApplicationServices.CreateScope())
                {

                    var service = scope.ServiceProvider.GetService<ContextSeedData>();
                    service.Seed();

                }
            }
        }
    }


    
}
