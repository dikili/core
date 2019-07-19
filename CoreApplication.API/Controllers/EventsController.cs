using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CoreApplication.API.Utility;
using CoreApplication.Data.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace CoreApplication.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class EventsController : ControllerBase
    {
        private readonly IDatingRepository _repo;
        private readonly IMapper _mapper;
        private IMemoryCache _memoryCacheService;

        public EventsController(IDatingRepository repo, IMapper mapper, IMemoryCache memoryCacheService)
        {
            _mapper = mapper;
            _repo = repo;
            _memoryCacheService = memoryCacheService;
        }

        [HttpGet]
        public async Task<IActionResult> GetEvents()
        {
            //var isCurrentUser = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value) == userId;

            //var user = await _repo.GetUser(userId, isCurrentUser);

           var eventsSection= Extractor.ParsePage("https://canarywharf.com/arts-events/events/", ".listing future-event");
            // jobsToReturn = jobsToReturn.OrderByDescending(x => (x.Date)).ToList(); //_mapper.Map<JobListDto>(jobs);
            return Ok(eventsSection.Result);
        }
    }
}