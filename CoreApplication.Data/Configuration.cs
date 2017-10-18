using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreApplication.Data
{
    public  class Configuration
    {
        private static IConfigurationRoot _config;

        public  Configuration(IConfigurationRoot config)
        {
            _config = config;
        }

        public string GetConnectionString()
        {
           // var res= _config["ConnectionStrings:CoreContextConnection"];

            return "Server=localhost\\SQLEXPRESS;Database=newone;Trusted_Connection=True;";
        }
    }
}
