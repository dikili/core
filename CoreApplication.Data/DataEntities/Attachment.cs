using System;
using System.Collections.Generic;
using System.Text;
using CoreApplication.Data.Models;

namespace CoreApplication.Data.DataEntities
{
    public class Attachment :BaseEntity
    {
        public string ImagePath { get; set; }

        public virtual Ad Ad { get; set; }

    }
}
