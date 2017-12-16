using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreApplication.Data.DataEntities;
using CoreApplicationTest.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CoreApplicationTest.Controllers.Web
{
    public class AccountController :Controller
    {
        private ILogger<AccountController> _logger;
        private SignInManager<AdUser> _signInManager;

        public AccountController(ILogger<AccountController> logger,SignInManager<AdUser> signInManager)
        {
            _logger = logger;
            _signInManager = signInManager;
        }

        public IActionResult Login()
        {
            if (this.User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "App");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result =  await _signInManager.PasswordSignInAsync(model.UserName,
                    model.Password,
                    model.RememberMe,
                    false);

                if (result.Succeeded)
                {
                    // When Authorize attribute is used returnurl is appended as we saw
                    // If that happens we can say that redirect to returnUrl if it exist if not go to the default as in else below
                    if (Request.Query.Keys.Contains("ReturnUrl"))
                    {
                       return Redirect(Request.Query["ReturnUrl"].First());
                    }
                    //else
                    //{
                       return RedirectToAction("Index", "App");
                   // }
                    
                }
            }

            ModelState.AddModelError("","Failed to Login");
           // var user = await _signInManager.UserManager.FindByNameAsync(model.UserName);

            //var res= _signInManager.SignInAsync(user, true);
            //unless the sign in successful we should go to the View
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
           return RedirectToAction("Index", "App");
        }
    }
}
