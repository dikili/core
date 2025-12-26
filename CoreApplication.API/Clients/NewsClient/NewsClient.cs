using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreApplication.API.DTOs;
using Microsoft.Extensions.Caching.Memory;
using RestSharp;
using RestSharp.Deserializers;

namespace CoreApplication.API.Clients.NewsClient
{
    public class NewsClient : BaseClient
    {
        public NewsClient(IDeserializer serializer, IMemoryCache service, string baseUrl) : base(serializer, service, baseUrl)
        {
        }

        public News GetNewsByCache()
        {
            // RestRequest request = new RestRequest("v2/top-headlines", Method.GET);
            // request.AddParameter("country", "gb");
            // request.AddParameter("category", "business");
            // request.AddParameter("apiKey", "8b5df5fd3a5245c9af5c2fabcc9a6e96");
            RestRequest request = new RestRequest("v2/everything", Method.GET);
            request.AddParameter("q", "business");
            request.AddParameter("language", "en");
            request.AddParameter("sortBy", "publishedAt");
            request.AddParameter("apiKey", "8b5df5fd3a5245c9af5c2fabcc9a6e96");

            //request.AddHeader("Authorization", "Basic Yjk3YzBiOTgtYjQzMy00NGI0LTlmNTgtNWQ3NGFiYjMwYjJkOg==");

            return GetFromCache<News>(request, "News");

            //foreach (var w in news.Articles)
            //{
            //    if (!w.UrlToImage.StartsWith("https"))
            //        w.UrlToImage = "";
            //}
            //return news;

        }

        public News GetLocalNewsByCache()
        {
            RestRequest request = new RestRequest("v2/everything", Method.GET);
            request.AddParameter("q", "canary wharf");
            request.AddParameter("language", "en");
            request.AddParameter("sortBy", "publishedAt");
            request.AddParameter("apiKey", "8b5df5fd3a5245c9af5c2fabcc9a6e96");


            //request.AddHeader("Authorization", "Basic Yjk3YzBiOTgtYjQzMy00NGI0LTlmNTgtNWQ3NGFiYjMwYjJkOg==");

            return GetFromCache<News>(request, "LocalNews");

            //foreach (var w in news.Articles)
            //{
            //    if (!w.UrlToImage.StartsWith("https"))
            //        w.UrlToImage = "";
            //}
            //return news;

        }

        public News GetCustomNewsByCache(string phrase)
        {
            RestRequest request = new RestRequest("v2/everything", Method.GET);
            request.AddParameter("q", phrase);
            request.AddParameter("language", "en");
            request.AddParameter("apiKey", "8b5df5fd3a5245c9af5c2fabcc9a6e96");


            //request.AddHeader("Authorization", "Basic Yjk3YzBiOTgtYjQzMy00NGI0LTlmNTgtNWQ3NGFiYjMwYjJkOg==");

            var news = GetFromCache<News>(request, "CustomNews");

            foreach (var w in news.Articles)
            {
                if (w?.UrlToImage != null && !w.UrlToImage.StartsWith("https"))
                    w.UrlToImage = "";
            }
            return news;

        }
    }
}
