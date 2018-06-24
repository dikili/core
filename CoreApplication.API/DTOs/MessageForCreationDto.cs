using System;

namespace CoreApplication.API.DTOs
{
    public class MessageForCreationDto
    {
        public int SenderId { get; set; }

        public int ReceipentId { get; set; }

       public DateTime MessageSent { get; set; }

       public string Content { get; set; }

       public MessageForCreationDto()
       {
           MessageSent= DateTime.Now;
       }

       
    }
}