using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CoreApplicationTest.ViewModels;
using CoreApplicationTest.Services;
using Microsoft.Extensions.Configuration;

using CoreApplication.Data;
using CoreApplication.Data.Repositories;
using CoreApplication.Data.Models;
using CoreApplication.Data.Repositories.Interfaces;

namespace CoreApplicationTest.Controllers.Web
{
    public class AppController : Controller
    {
        private IMailService _mailService;
        private IConfiguration _config;
        //  private CoreApplication.Data.CoreContext _context;

        private ICoreRepository<Trip> _coreRepo;

        public AppController(IMailService service,IConfiguration config, ICoreRepository<Trip> coreRepo)
        {
            _mailService = service;
            _config = config;
            _coreRepo = coreRepo;
        }
        public IActionResult Index()
        {
            //  var data = _context.Trips.ToList();
            var result = _coreRepo.Count();
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Contact(ContactViewModel model)
        {
            if(model.Email.Contains("aol.com"))
            {
                ModelState.AddModelError("Email", "We dont support AOL addresses");
                // you can pass the first parameter as empty then the error will appear in the
                // summary section rather than next to the Email field

            }

            if(ModelState.IsValid)
            { 
                 _mailService.SendMail(_config["MailSettings:ToAddress"], "b", "c", "d");

                ModelState.Clear();

                ViewBag.UserMessage = "Message Sent";
            }
            return View();
        }

        public IActionResult About()
        {
            return View();
        }
    }
}