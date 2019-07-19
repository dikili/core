using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace CoreApplication.API.Utility
{
    public static class Extractor
    {
        public async static Task<IEnumerable<HtmlNode>> ParsePage(string pageUrl,string selector)
        {
            //  Uri pageUri = new Uri(pageUrl);

            //  // CrawledPage _crawledPage = new PageRequester(new CrawlConfiguration()).MakeRequest(new Uri(pageUrl));
            ////  CrawledPage page = new CrawledPage(pageUri);  // new PageRequester(new CrawlConfiguration()).MakeRequest(new Uri(pageUrl));
            //  PageContent result = null;
            //  using (WebResponse response = WebRequest.Create(pageUri).GetResponse())
            //  {
            //      result = new WebContentExtractor().GetContent(response);
            //  }

            //  //page.Content = result;

            //  //return TrimPage(page, selector);

            //  return result;

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
                  
                   var another = document.DocumentNode.SelectNodes("//*[contains(@class,selector)]");
                   return another;
                    //Some work with page....
                }
            }

        }

        /// <summary>
        /// id or the css needs to exist in the page
        /// </summary>
        /// <param name="page"></param>
        /// <param name="cssSelector"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        //private static IEnumerable<IElement> TrimPage(CrawledPage page, string cssSelector="",string id="")
        //{
        //    IEnumerable<IElement> interestedSection=null;

        //    if(!string.IsNullOrEmpty(cssSelector)) interestedSection = page.AngleSharpHtmlDocument.All
        //                                                                .Where(m => m.GetAttribute("class") == cssSelector);

        //    if (!string.IsNullOrEmpty(id)) interestedSection = page.AngleSharpHtmlDocument.All
        //                                                           .Where(m => m.GetAttribute("id") == id);

        //    return interestedSection;
        //}
    }
}
