using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using CoreApplication.API.DTOs;
using CoreApplication.Data.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoreApplication.API.Controllers
{
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
        public IActionResult GetUsers()
        {
            var users= _userRepo.GetUsers();

            var userToReturn = _mapper.Map<IEnumerable<UserForListDto>>(users);

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
        
    }
}