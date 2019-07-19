using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using CoreApplication.API.Clients.JobsClient;
using CoreApplication.API.DTOs;
using CoreApplication.API.ServiceModels;
using CoreApplication.Data.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using RestSharp.Serialization.Json;


namespace CoreApplication.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class JobsController : ControllerBase
    {
        private readonly IDatingRepository _repo;
        private readonly IMapper _mapper;
       private IMemoryCache _memoryCacheService;
        public JobsController(IDatingRepository repo, IMapper mapper,IMemoryCache memoryCacheService)
        {
            _mapper = mapper;
            _repo = repo;
            _memoryCacheService = memoryCacheService;
        }
        [HttpGet]
        public async Task<IActionResult> GetJobs()
        {
            //var isCurrentUser = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value) == userId;

            //var user = await _repo.GetUser(userId, isCurrentUser);

            var client = new JobsClient(new JsonDeserializer(), _memoryCacheService);

            var jobs = client.GetJobsByCache();
            //  foreach (var job in jobs)
            //   {
            //       job.Date=Convert.ToDateTime(job.Date,CultureInfo.InvariantCulture);
            //   }
              var jobsToReturn = _mapper.Map<List<JobDto>>(jobs);

           
            var jobswithdatelst = new List<JobwithDate>();
            foreach(var job in jobsToReturn)
            {
                var jobwithdate = new JobwithDate();
                jobwithdate.Date = new DateTime(Convert.ToInt32(job.Date.Substring(6,4)), Convert.ToInt32(job.Date.Substring(3, 2)), Convert.ToInt32(job.Date.Substring(0, 2)));
                jobwithdate.ExpirationDate = new DateTime(Convert.ToInt32(job.ExpirationDate.Substring(6, 4)), Convert.ToInt32(job.ExpirationDate.Substring(3, 2)), Convert.ToInt32(job.ExpirationDate.Substring(0, 2)));
                jobwithdate.JobDescription = job.JobDescription;
                jobwithdate.JobTitle = job.JobTitle;
                jobwithdate.JobUrl = job.JobUrl;
                jobwithdate.LocationName = job.LocationName;
                jobwithdate.EmployerName = job.EmployerName;
                jobwithdate.Applications = job.Applications;

                jobswithdatelst.Add(jobwithdate);


            }
             // jobsToReturn = jobsToReturn.OrderByDescending(x => (x.Date)).ToList(); //_mapper.Map<JobListDto>(jobs);
            return Ok(jobswithdatelst);    
        }

    }
}