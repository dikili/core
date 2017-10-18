using CoreApplication.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreApplication.Data
{
    public class CoreContext : DbContext
    {
         private IConfigurationRoot _config;

        //public CoreContext(DbContextOptions<CoreContext> options)
        //    :base(options)
        //{
        //   // _config = config;
        //}
        public CoreContext() { }
       
        public DbSet<Trip> Trips { get; set; }
        public DbSet<Stop> Stops { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDb;Database=AnotherCoreDb;Trusted_Connection=true;MultipleActiveResultSets=true;");

        }
    }
}
