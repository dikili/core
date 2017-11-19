using System;
using System.Collections.Generic;
using System.Text;
using CoreApplication.Data.Models;

namespace CoreApplication.Data.DataEntities
{
    public class Response :BaseEntity
    {
        public string UserName { get; set; }

        public int AdId { get; set; }

        public DateTime ResponseDate { get; set; }

        public string FreeText { get; set; }

        public virtual Ad Ad { get; set; }
    }
}
