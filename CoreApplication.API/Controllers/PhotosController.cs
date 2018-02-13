using AutoMapper;
using CloudinaryDotNet;
using CoreApplication.API.Helpers;
using CoreApplication.Data.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace CoreApplication.API.Controllers
{
    // uploading the photos functionality for the users
    [Authorize]
    [Route("users/{userid}/photos")]
    public class PhotosController : Controller
    {
        private readonly Mapper _mapper;
        private readonly CloudinarySettings _cloudinaryConfig;
        public PhotosController(Mapper mapper, CloudinarySettings cloudinaryConfig,IDatingRepository userRepo)
        {
            _cloudinaryConfig = cloudinaryConfig;
            _mapper = mapper;
            
            Account account=new Account();
        }
    }
}