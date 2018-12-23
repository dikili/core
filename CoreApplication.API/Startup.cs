using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using CoreApplication.Data.Repositories;
using CoreApplication.Data.Repositories.Interfaces;
using CoreApplication.Data;
using CoreApplication.Data.DataEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Net;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CoreApplication.API.Helpers;
using CoreApplication.Data.Settings;
using AutoMapper;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;

namespace CoreApplication.API
{
    public class Startup
    {
        private IHostingEnvironment _env;
        // private IConfigurationRoot _config;


        private IConfiguration _config { get; }

        public Startup(IConfiguration config, IHostingEnvironment env)
        {
            _env = env;
            //var builder = new ConfigurationBuilder()
            //    .SetBasePath(_env.ContentRootPath)
            //    .AddJsonFile("config.json")
            //    .AddEnvironmentVariables();

            _config = config;
            // Configuration = config;
        }

        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;

        }
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //  services.AddDbContext<CoreContext>(x=>x.UseSqlite(Configuration.GetConnectionString("DefaultConnection")));
            var key = Encoding.ASCII.GetBytes(_config.GetSection("AppSettings:Token").Value);
            services.AddDbContext<CoreContext>(options =>
            options.UseSqlServer(_config.GetConnectionString("CoreContextConnection"))
            .ConfigureWarnings(warnings => warnings.Ignore(CoreEventId.IncludeIgnoredWarning)));

            services.AddTransient<Seed>();
            // if this app was MVC rather than the angular app we would use AddIdentity insted
            IdentityBuilder builder = services.AddIdentityCore<LoginUser>(opt =>
            {
                opt.Password.RequireDigit = false;
                opt.Password.RequiredLength = 4;
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequireUppercase = false;
            });

            // if above was AddIdentity none of the below would be needed !!
            // by default signinmanager,rolemanager,validator would be added
            builder = new IdentityBuilder(builder.UserType, typeof(Role), builder.Services);
            builder.AddEntityFrameworkStores<CoreContext>();
            builder.AddRoleValidator<RoleValidator<Role>>();
            builder.AddRoleManager<RoleManager<Role>>();
            builder.AddSignInManager<SignInManager<LoginUser>>();


            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
               .AddJwtBearer(options =>
               {
                   options.TokenValidationParameters = new TokenValidationParameters
                   {
                       ValidateIssuerSigningKey = true,
                       IssuerSigningKey = new SymmetricSecurityKey(key),
                       ValidateIssuer = false,
                       ValidateAudience = false
                   };
               });



            services.Configure<CloudinarySettings>(_config.GetSection("CloudinarySettings"));
            
            // Add a new Authorize Filter so eliminate the need to add an Authorize attribute each time on the controllers !!
            services.AddMvc( options =>
            {
                var policy = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .Build();
                options.Filters.Add(new AuthorizeFilter(policy));
            }
                ).SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
            .AddJsonOptions(opt => 
            {
              opt.SerializerSettings.ReferenceLoopHandling=Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });

            // Will apply any pending migrations to database and will create db if does not already exist when below is done..
            services.BuildServiceProvider().GetService<CoreContext>().Database.Migrate();
            services.AddCors();
            
            services.AddScoped<IDatingRepository,DatingRepository>();

            
               services.AddAutoMapper();

               
        }
        public void ConfigureDevelopmentServices(IServiceCollection services)
        {
            //  services.AddDbContext<CoreContext>(x=>x.UseSqlite(Configuration.GetConnectionString("DefaultConnection")));
            var key = Encoding.ASCII.GetBytes(_config.GetSection("AppSettings:Token").Value);
            services.AddDbContext<CoreContext>(options =>
            options.UseSqlServer(_config.GetConnectionString("CoreContextConnection"))
            .ConfigureWarnings(warnings => warnings.Ignore(CoreEventId.IncludeIgnoredWarning)));
           
             services.Configure<CloudinarySettings>(_config.GetSection("CloudinarySettings"));

            services.AddTransient<Seed>();


            services.AddTransient<Seed>();
            // if this app was MVC rather than the angular app we would use AddIdentity insted
            IdentityBuilder builder = services.AddIdentityCore<LoginUser>(opt =>
            {
                opt.Password.RequireDigit = false;
                opt.Password.RequiredLength = 4;
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequireUppercase = false;
            });

            // if above was AddIdentity none of the below would be needed !!
            // by default signinmanager,rolemanager,validator would be added
            builder = new IdentityBuilder(builder.UserType, typeof(Role), builder.Services);
            builder.AddEntityFrameworkStores<CoreContext>();
            builder.AddRoleValidator<RoleValidator<Role>>();
            builder.AddRoleManager<RoleManager<Role>>();
            builder.AddSignInManager<SignInManager<LoginUser>>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
              .AddJwtBearer(options =>
              {
                  options.TokenValidationParameters = new TokenValidationParameters
                  {
                      ValidateIssuerSigningKey = true,
                      IssuerSigningKey = new SymmetricSecurityKey(key),
                      ValidateIssuer = false,
                      ValidateAudience = false
                  };
              });



            services.AddMvc().AddJsonOptions(opt => 
            {
              opt.SerializerSettings.ReferenceLoopHandling=Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });
            services.AddCors();
            

            services.AddScoped<IDatingRepository,DatingRepository>();

           
           
            services.AddAutoMapper();

               
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, Seed seeder)
        {
            app.UseDeveloperExceptionPage();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                //global exception handler if in the production etc..
                //this is the error that will be seen ...
                //  
                app.UseExceptionHandler(builder =>
                {
                    builder.Run(async context =>
                    {
                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                        var error = context.Features.Get<IExceptionHandlerFeature>();

                        if (error != null)
                        {
                            context.Response.AddApplicationErrors(error.Error.Message); //.AddApplicationError(error.Error.Message);
                            await context.Response.WriteAsync(error.Error.Message);
                        }

                    });
                });
            }
            // just enable if you want the seed data to work...
            seeder.SeedData();
            app.UseCors(p => p.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin().AllowCredentials());
            app.UseAuthentication();
          
            app.UseDefaultFiles(); // for deployment necassary
            app.UseStaticFiles();
            app.UseMvc(routes => {
                routes.MapSpaFallbackRoute(
                    name: "spa-fallback",
                    defaults: new { controller ="Fallback", Action="Index"}
                );
            });

           
        }
    }
}
