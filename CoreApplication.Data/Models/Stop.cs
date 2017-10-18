using System;
using System.Collections.Generic;
using System.Text;

namespace CoreApplication.Data.Models
{
    public class Stop
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public double Latitude { get; set; }

        public double Longtitude { get; set; }

        public int Order { get; set; }

        public DateTime Arrival { get; set; }
    }
}
