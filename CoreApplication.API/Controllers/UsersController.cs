using System.Collections.Generic;
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
        public async Task<IActionResult> GetUsers()
        {
            var users=await _userRepo.GetUsers();

            var userToReturn = _mapper.Map<IEnumerable<UserForListDto>>(users);

            return Ok(userToReturn);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user=await _userRepo.GetUser(id);

            var userToReturn=_mapper.Map<UserForDetailedDto>(user);

            return Ok(userToReturn);
        }
        
    }
}