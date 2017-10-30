using System;
using System.Collections.Generic;
using System.Text;
using CoreApplication.Data.DataEntities.Interfaces;

namespace CoreApplication.Data.DataEntities
{
    public class BaseEntity :IEntity<int>
    {
        public int Id { get; set; }
    }
}
