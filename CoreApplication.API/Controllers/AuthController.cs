using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
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
        private readonly IMapper _mapper;

        public AuthController(IAuthRepository repo,IMapper mapper)
        {
            _repo = repo;
            _mapper=mapper;
            
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

            // var userToCreate = new LoginUser
            // {
            //     UserName = username
            // };

             var userToCreate =  _mapper.Map<LoginUser>(user);
             
             // created user has password etc.. which we do not want to return
             // so we make another conversion
              var createUser=await _repo.Register(userToCreate,user.Password); 

            var userToReturn = _mapper.Map<UserForDetailedDto>(createUser);

              return CreatedAtRoute("GetUser",new { controller="Users", Id= userToReturn.Id },userToReturn);
           // return CreatedAtRoute()

           // return StatusCode(201);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody]UserForLoginDto user)
        {
          
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

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

             var mappedUser = _mapper.Map<UserForListDto>(userFromRepo);

             var tokenString=tokenHandler.WriteToken(token);

             return Ok(new {tokenString,mappedUser}); 
         
        }

    }
}