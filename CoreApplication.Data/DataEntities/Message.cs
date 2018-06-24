using System;

namespace CoreApplication.Data.DataEntities
{
    public class Message
    {
        public int Id { get; set; }

        public int SenderId { get; set; }

       public LoginUser Sender { get; set; }

        public int ReciepentId { get; set; }

        public LoginUser Receiver { get; set; }

        public string Content { get; set; }

        public bool IsRead { get; set; }

        public bool SenderDeleted { get; set; }

        public bool ReciepentDeleted { get; set; }

        public DateTime MessageSent { get; set; }

        public DateTime? DateRead { get; set; }
        
    }
}