using System;
using System.Collections.Generic;
using System.Text;
using CoreApplication.Data.DataEntities;

namespace CoreApplication.Data.Models
{
    public class Stop :BaseEntity
    {

        public string Name { get; set; }

        public double Latitude { get; set; }

        public double Longtitude { get; set; }

        public int Order { get; set; }

        public DateTime Arrival { get; set; }
    }
}
