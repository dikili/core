using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreApplication.API.Cache
{
    public class InMemoryCache : ICacheService
    {
        private IMemoryCache memoryCacheService;
        public InMemoryCache(IMemoryCache service)
        {
            memoryCacheService = service;
        }
        public T Get<T>(string cacheKey) where T : class
        {
            return memoryCacheService.Get(cacheKey) as T;
        }
        public void Set(string cacheKey, object item, int minutes)
        {
            if (item != null) memoryCacheService.Set(cacheKey, item, DateTime.Now.AddMinutes(30));  //.CreateEntry(item);  //(cacheKey, item, DateTime.Now.AddMinutes(30));


        }

    }
}

