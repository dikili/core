using CoreApplication.API.Utility.Concrete;
using CoreApplication.API.Utility.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreApplication.API.Utility.Factory
{
    public static class ExtractorFactory //: IExtractorFactory
    {
        public static IExtractor Resolve(string extractorType = "none")
        {
            switch (extractorType)
            {
                case "event": return new EventsExtractor();
                default: return new EventsExtractor();
            }
        }
    }
}
