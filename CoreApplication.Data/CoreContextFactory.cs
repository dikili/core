using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace CoreApplication.Data
{
    //public class DesignTimeDbContextFactory //: IDesignTimeDbContextFactory<CoreContext>
    //{
        //private IConfigurationRoot _config;

        //public CoreContextFactory(IConfigurationRoot config)
        //{
        //    _config = config;

        //}
        //public CoreContext CreateDbContext(string[] args)
        //{
        //    var builder = new DbContextOptionsBuilder<CoreContext>();

        //    builder.UseSqlServer(_config["ConnectionStrings:CoreContextConnection"]);
           
        //    return new CoreContext(builder.Options);
        //}
    //}

    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<CoreApplication.Data.CoreContext>
    {
        public CoreApplication.Data.CoreContext CreateDbContext(string[] args)
        {
            var currentPath = Directory.GetCurrentDirectory();

            var mainDir = Directory.GetDirectoryRoot(currentPath);

            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(currentPath)
                .AddJsonFile("~/../config.json")
                .Build();
            var builder = new DbContextOptionsBuilder<CoreApplication.Data.CoreContext>();
            var connectionString = configuration.GetConnectionString("CoreContextConnection");
            builder.UseSqlServer(connectionString);
            var coreContext= new CoreContext(builder.Options);
            //var seeder = new ContextSeedData(coreContext);
            //seeder.EnsureDataSeed().Wait();
            return coreContext;
        }
    }
}
