using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CoreApplication.API.Clients.NewsClient;
using CoreApplication.API.DTOs;
using CoreApplication.Data.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using RestSharp.Serialization.Json;

namespace CoreApplication.API.Controllers
{
    [Route("api")]
    [ApiController]
    [AllowAnonymous]
    public class NewsController : ControllerBase
    {
         
       private readonly IDatingRepository _repo;
        private readonly IMapper _mapper;
        private IMemoryCache _memoryCacheService;

        public NewsController(IDatingRepository repo, IMapper mapper, IMemoryCache memoryCacheService)
        {
              _mapper = mapper;
            _repo = repo;
            _memoryCacheService = memoryCacheService;
        }


        
        [HttpGet("news")]
        public async Task<IActionResult> GetNews()
        {
            //var isCurrentUser = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value) == userId;

            //var user = await _repo.GetUser(userId, isCurrentUser);



            var client = new NewsClient(new JsonDeserializer(), _memoryCacheService, "https://newsapi.org/");

            var news = client.GetNewsByCache();
            //  foreach (var job in jobs)
            //   {
            //       job.Date=Convert.ToDateTime(job.Date,CultureInfo.InvariantCulture);
            //   }
            // var jobsToReturn = _mapper.Map<List<Article>>(news);

            return Ok(news);
        }

        [HttpGet("localnews")] 
        public async Task<IActionResult> GetLocalNews()
        {
            var client = new NewsClient(new JsonDeserializer(), _memoryCacheService, "https://newsapi.org/");

            var news = client.GetLocalNewsByCache();
            //  foreach (var job in jobs)
            //   {
            //       job.Date=Convert.ToDateTime(job.Date,CultureInfo.InvariantCulture);
            //   }
            // var jobsToReturn = _mapper.Map<List<Article>>(news);

            return Ok(news);
        }

        [HttpGet("customnews")]
        public async Task<IActionResult> GetCustomNews(string phrase)
        {
            var client = new NewsClient(new JsonDeserializer(), _memoryCacheService, "https://newsapi.org/");

            var news = client.GetCustomNewsByCache(phrase);
            //  foreach (var job in jobs)
            //   {
            //       job.Date=Convert.ToDateTime(job.Date,CultureInfo.InvariantCulture);
            //   }
            // var jobsToReturn = _mapper.Map<List<Article>>(news);

            return Ok(news);
        }

        [HttpGet]
        public async Task<IActionResult> GetCustomNews()
        {
            return null;
        }
    }
}