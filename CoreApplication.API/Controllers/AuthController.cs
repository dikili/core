using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using CoreApplication.API.DTOs;
using CoreApplication.Data.DataEntities;
using CoreApplication.Data.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace CoreApplication.API.Controllers
{
    [Route("api/[Controller]")]
    [AllowAnonymous]
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
           string username="";
           if(!string.IsNullOrEmpty(user.Username))
                 username=user.Username.ToLower();
              
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

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody]UserForLoginDto user)
        {
            throw new Exception("Computer says no");
   
             var userFromRepo=await _repo.Login(user.UserName,user.Password);

             if(userFromRepo==null)
               return Unauthorized();

             var tokenHandler=new JwtSecurityTokenHandler(); 

             var key=Encoding.ASCII.GetBytes("super secret key");

             var tokenDescriptor=new SecurityTokenDescriptor{
                 Subject= new ClaimsIdentity(new Claim[]{
                     new Claim(ClaimTypes.NameIdentifier,userFromRepo.Id.ToString()),
                     new Claim(ClaimTypes.Name,userFromRepo.UserName)
                 }),
                 Expires=DateTime.Now.AddDays(1),
                 SigningCredentials=new SigningCredentials(new SymmetricSecurityKey(key),SecurityAlgorithms.HmacSha512Signature)
             };

             var token=tokenHandler.CreateToken(tokenDescriptor);

             var tokenString=tokenHandler.WriteToken(token);

             return Ok(new {tokenString}); 
         
        }

    }
}