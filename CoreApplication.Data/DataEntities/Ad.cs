using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreApplication.Data.DataEntities;

namespace CoreApplication.Data.Models
{
    public class Ad :BaseEntity
    {
        public string Title { get; set; }
        public string UserId { get; set; }
        public string Reason { get; set; }
        public string Details { get; set; }
        public DateTime AdPostDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public int CategoryId { get; set; }
        public double Rating { get; set; }
        public bool IsActive { get; set; }
        public virtual AdUser User { get; set; }
        public virtual ICollection<Response> Responses { get; set; }
        public virtual ICollection<Attachment> Attachments { get; set; }
    }
}
