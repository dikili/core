using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using CoreApplication.API.DTOs;
using CoreApplication.API.Helpers;
using CoreApplication.Data.DataEntities;
using CoreApplication.Data.Helpers;
using CoreApplication.Data.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoreApplication.API.Controllers
{
   // [ServiceFilter(typeof(LogUserActivity))]
    [Authorize]
    [Route("api/[controller]")]
    public class UsersController :Controller
    {
        private readonly IDatingRepository _userRepo;
        private readonly IMapper _mapper;

        public UsersController(IDatingRepository repository,IMapper mapper)
        {
            _userRepo=repository;
            _mapper=mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers(UserParams userParams)
        {

            var currentUserId=int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
           
            var currentUser=await _userRepo.GetUser(currentUserId);

            userParams.UserId = currentUserId;

            if(string.IsNullOrEmpty(userParams.Gender))
            { 
              userParams.Gender= currentUser.Gender == "male" ? "female" : "male"; 
            }
           

            var users= _userRepo.GetUsers(userParams).Result;

            var userToReturn = _mapper.Map<IEnumerable<UserForListDto>>(users);

            Response.AddPagination(users.CurrentPage,users.PageSize,users.TotalCount,users.TotalPages);

            return Ok(userToReturn);
        }

        [HttpGet("{id}", Name="GetUser")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user=await _userRepo.GetUser(id);

            var userToReturn=_mapper.Map<UserForDetailedDto>(user);

            return Ok(userToReturn);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id,[FromBody] UserForUpdateDto user)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
           
           // Get the current logged in user's id

            var currentUserId=int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
           
            var mainUser=await _userRepo.GetUser(id);

            if(mainUser.Id!=currentUserId)
            {
                return Unauthorized();
            }

             _mapper.Map(user, mainUser);

            if(await _userRepo.SaveAll())
                return NoContent();
           
           throw new Exception($"Update for userid {id} failed");
        }

        [HttpPost("{id}/like/{recepientId}")]
        public async Task<IActionResult> LikeUser(int id,int recepientId)
        {
             var currentUserId=int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

             if(id!=currentUserId)
               return Unauthorized();

             var like=_userRepo.GetLike(id,recepientId).Result;  

             if(like!=null)
               return BadRequest("You already liked this user!!");

              like  = new Like
              {
                  LikerId=id,
                  LikeeId=recepientId  
              }; 
            
              _userRepo.Add<Like>(like);

              if(await _userRepo.SaveAll())
              return Ok(new {});

              return BadRequest("Failed to add user");
        }

        }

  
    }