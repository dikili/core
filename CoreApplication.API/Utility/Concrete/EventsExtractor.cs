using System.Collections.Generic;
using System.Linq;
using CoreApplication.API.DTOs;
using CoreApplication.API.Utility.Interfaces;
using HtmlAgilityPack;

namespace CoreApplication.API.Utility.Concrete
{
    public class EventsExtractor : Extractor,IEventsExtractor
    {
        public EventsExtractor()
        {
        }


        public override IEnumerable<T> GetAllEntities<T>(string pageUrl, string selector)
        {
            var eventsDOM = base.ParsePageAsync(pageUrl, selector);
            var events = new List<Event>();

            foreach (var ev in eventsDOM.Result)
            {
                var eve = new Event();

                if (ev.NodeType == HtmlNodeType.Element)
                {
                    eve.ImgUrl = ev.Descendants("img").ElementAt(0).GetAttributeValue("src", string.Empty);
                    eve.Description = ev.Descendants("div").ElementAt(1).Descendants("p").ElementAt(1).InnerText;
                    eve.HeadLine = ev.Descendants("a").ElementAt(0).InnerText;
                    eve.MoreLink = ev.Descendants("a").ElementAt(0).GetAttributeValue("href", string.Empty);
                    eve.DateInfo = ev.Descendants("div").ElementAt(1).Descendants("p").ElementAt(0).InnerText;
                }


                events.Add(eve);
            }

            return events as IEnumerable<T>;
        }

      
    }
}
