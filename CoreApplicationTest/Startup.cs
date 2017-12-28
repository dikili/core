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
using System.Text;
using Microsoft.EntityFrameworkCore.Design;
using CoreApplication.Data.Repositories;
using CoreApplication.Data.Repositories.Interfaces;
using CoreApplication.Data;
using CoreApplication.Data.DataEntities;
using CoreApplication.Data.Uow;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

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
            services.AddIdentity<AdUser, IdentityRole>(cfg =>
                {
                    cfg.User.RequireUniqueEmail = true;
                    cfg.Password.RequireDigit = true;
                }
            ).AddEntityFrameworkStores<CoreContext>();

           // last bit to add token authentication
            services.AddAuthentication()
                .AddCookie()
                .AddJwtBearer(cfg =>//json web token authentication enabling after setting the token generation up we need to tell startup.cs what should the token be like
                {
                    cfg.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidIssuer = _config["Tokens:Issuer"],
                        ValidAudience = _config["Tokens:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]))
                    };
                });

            if (_env.IsEnvironment("Development") || _env.IsEnvironment("Testing"))
            {
                services.AddScoped<IMailService, DebugMailService>();
            }

            

            //else implement a production one for the mail service here

            // if the environment is production use https rather than http
            //below is a sample

            // RequireHttpsAttribute can be used in certain controllers
            // In certain Actions ...
            // but for our case below it will be through out the site...
            
            //services.AddMvc(opt =>
            //{
            //    if (_env.IsProduction())
            //    {
            //        opt.Filters.Add(new RequireHttpsAttribute());
            //    }
            //});

            services.AddMvc();


            services.AddSingleton(_config);

            services.AddDbContext<CoreContext>(options =>
            options.UseSqlServer(_config.GetConnectionString("CoreContextConnection")));

            services.AddTransient<IUnitOfWork, UnitOfWork>();

            services.AddTransient(typeof(ICoreRepository<>), typeof(CoreRepository<>));
            services.AddTransient<ContextSeedData>();

            services.AddCors();
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
            //authentication needs to be before MVC in the asp.net pipeline
            // this default assumes cookie based authentication..
            app.UseAuthentication();

            //For development reasons only we are allowing any request basically
            app.UseCors(p => p.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin().AllowCredentials());

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
                    service.Seed().Wait();

                }
            }
        }
    }


    
}
