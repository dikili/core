using System;

namespace CoreApplication.Data.DataEntities
{
    public class Photo : BaseEntity
    {
        public string Url { get; set; }

        public string Description { get; set; }

        public DateTime DateAdded { get; set; }

        public bool IsMain { get; set; }

        public string PublicId { get; set; }

        public LoginUser User { get; set; }

        public int LoginUserId { get; set; }
    }
}