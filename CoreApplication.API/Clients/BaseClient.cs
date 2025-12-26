using CoreApplication.API.Cache;
using CoreApplication.API.Helpers;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Deserializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreApplication.API.Clients
{
    public class BaseClient : RestClient
    {
        protected ICacheService _cache;
        protected IErrorLogger _errorLogger;

        public BaseClient(IDeserializer serializer,IMemoryCache service, string baseUrl)
        {
            _cache = new InMemoryCache(service);//cache; //cache;
            _errorLogger = new ErrorLogger(); //errorLogger;
            //AddHandler("application/json", serializer);
            //AddHandler("text/json", serializer);
            //AddHandler("text/x-json", serializer);
            BaseUrl = new Uri(baseUrl);
        }
        public override IRestResponse Execute(IRestRequest request)
        {
            var response = base.Execute(request);
            TimeoutCheck(request, response);
            return response;
        }
        public override IRestResponse<T> Execute<T>(IRestRequest request)
        {
            var response = base.Execute<T>(request);
            TimeoutCheck(request, response);
            return response;
        }

        public T Get<T>(IRestRequest request) where T : new()
        {
            var response = Execute<T>(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return response.Data;
            }
            else
            {
                LogError(BaseUrl, request, response);
                return default(T);
            }
        }

        public T GetFromCache<T>(IRestRequest request, string cacheKey) where T : class, new()
        {
            var item = _cache.Get<T>(cacheKey);
            if (true) //If the cache doesn't have the item
            {
                var response = Execute<T>(request); //Get the item from the API call
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                   _cache.Set(cacheKey, response.Content, 30); //Set that item into the cache so we can get it next time
                    item = JsonConvert.DeserializeObject<T>(response.Content);
                }
                else
                {
                    LogError(BaseUrl, request, response);
                    return default(T);
                }
            }
            return item;
        }
        private void LogError(Uri BaseUrl, IRestRequest request, IRestResponse response)
        {
            //Get the values of the parameters passed to the API
            string parameters = string.Join(", ", request.Parameters.Select(x => x.Name.ToString() + "=" + ((x.Value == null) ? "NULL" : x.Value)).ToArray());

            //Set up the information message with the URL, the status code, and the parameters.
            string info = "Request to " + BaseUrl.AbsoluteUri + request.Resource + " failed with status code " + response.StatusCode + ", parameters: "
                          + parameters + ", and content: " + response.Content;

            //Acquire the actual exception
            Exception ex;
            if (response != null && response.ErrorException != null)
            {
                ex = response.ErrorException;
            }
            else
            {
                ex = new Exception(info);
                info = string.Empty;
            }

            //Log the exception and info message
            _errorLogger.LogError(ex, info);
        }

        private void TimeoutCheck(IRestRequest request, IRestResponse response)
        {
            if (response.StatusCode == 0)
            {
                LogError(BaseUrl, request, response);
            }
        }
    }
}
