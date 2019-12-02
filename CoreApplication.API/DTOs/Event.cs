using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreApplication.API.DTOs
{
    public class Event
    {
        public string DateInfo { get; set; }
        public string ImgUrl { get; set; }
        public string HeadLine { get; set; }
        public string Description { get; set; }
        public string MoreLink { get; set; }
        //string imageUrl, wp-post-image
        //string headline, entry-title> a
        //string description, listing future-event > text > p
        //string more, listing future-event > text > p >a

        }
    }
