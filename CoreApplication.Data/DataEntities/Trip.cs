using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreApplication.Data.DataEntities;

namespace CoreApplication.Data.Models
{
    public class Trip :BaseEntity
    {

        public string Name { get; set; }

        public DateTime DateCreated { get; set; }

        public string UserName { get; set; }

        public ICollection<Stop> Stops { get; set; }
    }
}
