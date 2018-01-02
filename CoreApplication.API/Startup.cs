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

namespace CoreApplication.API
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

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
          //  services.AddDbContext<CoreContext>(x=>x.UseSqlite(Configuration.GetConnectionString("DefaultConnection")));
            var key =Encoding.ASCII.GetBytes(_config.GetSection("AppSettings:Token").Value);
            services.AddDbContext<CoreContext>(options =>
            options.UseSqlServer(_config.GetConnectionString("CoreContextConnection")));

            services.AddMvc();
            services.AddCors();

            services.AddScoped<IAuthRepository,AuthRepository>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
               .AddJwtBearer(options=>{
                   options.TokenValidationParameters=new TokenValidationParameters
                   {
                       ValidateIssuerSigningKey=true,
                       IssuerSigningKey=new SymmetricSecurityKey(key),
                       ValidateIssuer=false,
                       ValidateAudience=false
                   };
               });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors(p => p.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin().AllowCredentials());
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
