using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreApplication.API.DTOs;
using CoreApplication.API.ServiceModels;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Deserializers;

namespace CoreApplication.API.Clients.JobsClient
{
    public class JobsClient : BaseClient
    {
        public JobsClient(IDeserializer serializer, IMemoryCache service, string baseUrl= "") : base(serializer, service, "https://www.reed.co.uk/")
        {
        }

        public List<Job> GetJobsByCache()
        {
            RestRequest request = new RestRequest("api/1.0/search", Method.GET);
            request.AddParameter("locationName", "Canary Wharf");
            request.AddParameter("distancefromlocation", 0);

            request.AddHeader("Authorization", "Basic Yjk3YzBiOTgtYjQzMy00NGI0LTlmNTgtNWQ3NGFiYjMwYjJkOg==");

            return GetFromCache<JobList>(request,"CW").Results;

        }
    }
}
