using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreApplication.API.Cache
{
    public interface ICacheService
    {
            T Get<T>(string cacheKey) where T : class;

            void Set(string cacheKey, object item, int minutes);

    }
}
