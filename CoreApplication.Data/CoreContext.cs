using CoreApplication.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace CoreApplication.Data
{
    public class CoreContext : DbContext
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


        public DbSet<Trip> Trips { get; set; }
        public DbSet<Stop> Stops { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    base.OnConfiguring(optionsBuilder);
        //    var config = new Configuration(_config);

        //    optionsBuilder.UseSqlServer(config.GetConnectionString());

        //}
    }
}
