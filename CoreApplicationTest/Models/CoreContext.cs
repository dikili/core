using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreApplicationTest.Models
{
    public class CoreContext :DbContext
    {
        private IConfigurationRoot _config;

        public CoreContext(IConfigurationRoot config,DbContextOptions options) :base(options)
        {
            _config = config;
        }

        public DbSet<Trip> Trips { get; set; }

        public DbSet<Stop> Stops { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer(_config["ConnectionStrings:CoreContextConnection"]);

        }
    }
}
