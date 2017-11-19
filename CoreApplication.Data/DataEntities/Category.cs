using System;
using System.Collections.Generic;
using System.Text;
using CoreApplication.Data.DataEntities;

namespace CoreApplication.Data.Models
{
    public class Category :BaseEntity
    {

        public string Name { get; set; }
        public int Order { get; set; }
        public int IsValid { get; set; }
        public ICollection<Ad> Ads { get; set; }
    }
}
