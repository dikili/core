using CoreApplication.API.DTOs;
using CoreApplication.API.Utility.Interfaces;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace CoreApplication.API.Utility.Concrete
{
    public abstract class Extractor  : IExtractor
    {
        //  public abstract List<Event> GetAllEntities(string pageUrl, string selector);

        public abstract IEnumerable<T> GetAllEntities<T>(string pageUrl, string selector);
       

        public virtual async Task<IEnumerable<HtmlNode>> ParsePageAsync(string pageUrl, string selector)
        {
            HttpClient client = new HttpClient();

            // get answer in non-blocking way
            using (var response = await client.GetAsync(pageUrl))
            {
                using (var content = response.Content)
                {
                    // read answer in non-blocking way
                    var result = await content.ReadAsStringAsync();
                    var document = new HtmlDocument();
                    document.LoadHtml(result);

                    var another = document.DocumentNode.SelectNodes("//*[contains(@class,'" + selector + "')]");

                    var eventNodes = new List<HtmlNode>();
                    foreach (var node in another)
                    {
                        eventNodes.Add(node);

                    }
                    return eventNodes;

                }
            }
        }

     
    }
}
