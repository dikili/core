using CoreApplication.Data.DataEntities;
using CoreApplication.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;


namespace CoreApplication.Data
{
    public class CoreContext : IdentityDbContext<AdUser>
    {
         private static IConfigurationRoot _config { get; set; }

        //public CoreContext(DbContextOptions<CoreContext> options)
        //    :base(options)
        //{
        //   // _config = config;
        //}
        public CoreContext(DbContextOptions<CoreContext> options)
            : base(options)
        {
        
        }

        public DbSet<Ad> Ads { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Response> Responses { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    base.OnConfiguring(optionsBuilder);
        //    var config = new Configuration(_config);

        //    optionsBuilder.UseSqlServer(config.GetConnectionString());

        //}


    }
}
