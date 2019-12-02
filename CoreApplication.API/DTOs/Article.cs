using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreApplication.API.DTOs
{
    public class Article
    {
     

        //        author: string;
        //title: string;
        //description: string;
        //url: number;
        //urlToImage: string;
        //publishedAt: Date;
        //content: string;
        //source: Source;

        public string Author { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Url { get; set; }

        public string UrlToImage { get; set; }

        public string PublishedAt { get; set; }

        public string Content { get; set; }

        public string Source { get; set; }
    }

    public class Source
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
