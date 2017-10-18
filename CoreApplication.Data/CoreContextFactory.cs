using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreApplication.Data
{
    public class CoreContextFactory //: IDesignTimeDbContextFactory<CoreContext>
    {
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
    }
}
