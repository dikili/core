using CoreApplication.API.DTOs;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreApplication.API.Utility.Interfaces
{
    public interface IExtractor
    {
       // Task<IEnumerable<HtmlNode>> ParsePageAsync(string pageUrl, string selector);

        IEnumerable<T> GetAllEntities<T>(string pageUrl, string selector);
    }
}
