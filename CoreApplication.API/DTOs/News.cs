using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreApplication.API.DTOs
{
    [Serializable]
    public class News
    {
        //        status: string;
        //totalResults: number;
        //articles: Article[];#
        public string Status { get; set; }

        public int TotalResults { get; set; }

        public List<Article> Articles { get; set; }
    }
}
