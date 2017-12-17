using Microsoft.AspNetCore.Mvc;
using CoreApplicationTest.ViewModels;
using CoreApplicationTest.Services;
using Microsoft.Extensions.Configuration;
using CoreApplication.Data.Uow;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace CoreApplicationTest.Controllers.Web
{
    //[Route("api/[Controller]")]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class AppController : Controller
    {
        private IMailService _mailService;
        private IConfiguration _config;
        //  private CoreApplication.Data.CoreContext _context;

        private IUnitOfWork _uow;

        public AppController(IMailService service,IConfiguration config, IUnitOfWork uow)
        {
            _mailService = service;
            _config = config;
            _uow = uow;
        }

      [HttpGet]
        public IActionResult Index()
        {
            //  var data = _context.Trips.ToList();
            var result = _uow.AdRepository.Count(); //_coreRepo.Count();
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