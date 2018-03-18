using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using CoreApplication.Data.DataEntities;
using CoreApplicationTest.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace CoreApplicationTest.Controllers.Web
{
    public class AccountController :Controller
    {
        private ILogger<AccountController> _logger;
        // private SignInManager<AdUser> _signInManager;
        // private UserManager<AdUser> _userManager;
        private IConfiguration _config;

        public AccountController(ILogger<AccountController> logger,
            // SignInManager<AdUser> signInManager,
            // UserManager<AdUser> userManager,
            IConfiguration config)
        {
            _logger = logger;
            // _signInManager = signInManager;
            // _userManager = userManager;
            _config = config;
        }

        public IActionResult Login()
        {
            if (this.User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "App");
            }

            return View();
        }

        // [HttpPost]
        // public async Task<IActionResult> Login(LoginViewModel model)
        // {
        //     if (ModelState.IsValid)
        //     {
        //         //var result =  await _signInManager.PasswordSignInAsync(model.UserName,
        //         //     model.Password,
        //         //     model.RememberMe,
        //         //     false);

        //         // if (result.Succeeded)
        //         // {
        //         //     // When Authorize attribute is used returnurl is appended as we saw
        //         //     // If that happens we can say that redirect to returnUrl if it exist if not go to the default as in else below
        //         //     if (Request.Query.Keys.Contains("ReturnUrl"))
        //         //     {
        //         //        return Redirect(Request.Query["ReturnUrl"].First());
        //         //     }
        //         //     //else
        //         //     //{
        //         //        return RedirectToAction("Index", "App");
        //         //    // }
                    
        //         // }
        //     }

        //     ModelState.AddModelError("","Failed to Login");
        //    // var user = await _signInManager.UserManager.FindByNameAsync(model.UserName);

        //     //var res= _signInManager.SignInAsync(user, true);
        //     //unless the sign in successful we should go to the View
        //     return View();
        // }

        // [HttpGet]
        // public async Task<IActionResult> Logout()
        // {
        //    // await _signInManager.SignOutAsync();
        //    return RedirectToAction("Index", "App");
        // }

        // [HttpPost]
        // public async Task<IActionResult> CreateToken([FromBody] LoginViewModel model)
        // {
        //     // if (ModelState.IsValid)
        //     // {
        //     //     var user = await _userManager.FindByNameAsync(model.UserName);
        //     //     if (user != null)
        //     //     {
        //     //         var result =await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);
        //     //         if (result.Succeeded)
        //     //         {
        //     //             //Create Token

        //     //             //first we need to get the claims

        //     //             // claim is a type and a value..

        //     //             var claims = new []
        //     //             {
        //     //                 new Claim(JwtRegisteredClaimNames.Sub,user.Email),//subject
        //     //                 new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),// unique string to represent Token 
        //     //                 new Claim(JwtRegisteredClaimNames.UniqueName,user.UserName)  // unique name is the username of the user that is mapped to the identity that is inside the user object that is available in every controller
        //     //             };
        //     //             //used to encrypt token,when we read a token and when we generate the token
        //     //             //some part of the token is encrypted some are not, claims are not encrypted
        //     //             //but other parts like credentials or who it is binded might be encrypted
        //     //             var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));

        //     //             var creds=new SigningCredentials(key,SecurityAlgorithms.HmacSha256);

        //     //             var token =new JwtSecurityToken(
        //     //                 _config["Tokens:Issuer"],
        //     //                 _config["Tokens:Audience"],
        //     //                 claims,
        //     //                 expires:DateTime.UtcNow.AddMinutes(20),
        //     //                 signingCredentials:creds
                            
        //     //                 );

        //     //             var results = new
        //     //             {
        //     //                 token=new JwtSecurityTokenHandler().WriteToken(token),
        //     //                 expiration=token.ValidTo

        //     //             };

        //     //             return Created("", results);
        //     //         }
        //     //     }
              
        //     // }

        //     // return BadRequest();
        // }
    }
}
