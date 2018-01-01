using System.Threading.Tasks;
using CoreApplication.API.DTOs;
using CoreApplication.Data.DataEntities;
using CoreApplication.Data.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CoreApplication.API.Controllers
{
    [Route("api/[Controller]")]
    public class AuthController :Controller
    {
        private readonly IAuthRepository _repo;
        public AuthController(IAuthRepository repo)
        {
            _repo = repo;
            
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody]UserForRegisterDto user)
        {

            var username=user.Username.ToLower();
              
            if(await _repo.UserExists(username))
              ModelState.AddModelError("Username","Username already exists");

            //validate Request
           if(!ModelState.IsValid)
              return BadRequest(ModelState);

            var userToCreate = new LoginUser
            {
                UserName = username
            };


              var createUser=await _repo.Register(userToCreate,user.Password);
           // return CreatedAtRoute()

           return StatusCode(201);
        }

    }
}