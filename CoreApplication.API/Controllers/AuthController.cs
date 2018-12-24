using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CoreApplication.API.DTOs;
using CoreApplication.Data.DataEntities;
using CoreApplication.Data.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace DatingApp.API.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;
        private readonly UserManager<LoginUser> _userManager;
        private readonly SignInManager<LoginUser> _signInManager;
        public AuthController(IConfiguration config, 
            IMapper mapper,
            UserManager<LoginUser> userManager,
            SignInManager<LoginUser> signInManager)
        {
            _mapper = mapper;
            _config = config;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForRegisterDto userForRegisterDto)
        {
            // userForRegisterDto.Username = userForRegisterDto.Username.ToLower();

            // if (await _repo.UserExists(userForRegisterDto.Username))
            //     return BadRequest("Username already exists");

            var userToCreate = _mapper.Map<LoginUser>(userForRegisterDto);

            var result = await _userManager.CreateAsync(userToCreate,userForRegisterDto.Password);



            // var createdUser = await _repo.Register(userToCreate, userForRegisterDto.Password);

            var userToReturn = _mapper.Map<UserForDetailedDto>(userToCreate);

            if(result.Succeeded)
            {
               return CreatedAtRoute("GetUser", new {controller = "Users", id = userToCreate.Id}, userToReturn); 
            }
        
        return BadRequest(result.Errors);
            
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserForLoginDto userForLoginDto)
        {
            //var userFromRepo = await _repo.Login(userForLoginDto.UserName.ToLower(), userForLoginDto.Password);

            //if (userFromRepo == null)
            //    return Unauthorized();

            var user = await _userManager.FindByNameAsync(userForLoginDto.UserName);

            var result = await _signInManager.CheckPasswordSignInAsync(user, userForLoginDto.Password, false);
          
            if(result.Succeeded)
            {
                var appUser = await _userManager.Users.Include(p => p.Photos)
                    .FirstOrDefaultAsync(u => u.NormalizedUserName == userForLoginDto.UserName.ToUpper());

                var userToReturn = _mapper.Map<UserForListDto>(appUser);

                return Ok(new
                {
                    token = GenerateJwtToken(appUser).Result,//tokenHandler.WriteToken(token),
                    user = userToReturn
                });
            }


            return Unauthorized();
            //var user = _mapper.Map<UserForListDto>(userFromRepo);

            //return Ok(new
            //{
            //    token = GenerateJwtToken(userFromRepo),//tokenHandler.WriteToken(token),
            //    user
            //});
        }

        private async Task<string> GenerateJwtToken(LoginUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName)
            };


            var roles =  _userManager.GetRolesAsync(user).Result;

            foreach(var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }


            var key = new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes(_config.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
