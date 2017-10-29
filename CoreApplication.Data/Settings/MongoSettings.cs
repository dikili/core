using System;
using System.Collections.Generic;
using System.Text;

namespace CoreApplication.Data.Settings
{
    public class MongoSettings
    {
        public string User { get; set; }
        public string Pass { get; set; }
        public string Host { get; set; }
        public string Port { get; set; }
        public string Data { get; set; }
    }
}
