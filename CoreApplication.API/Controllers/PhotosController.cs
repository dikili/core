using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using CoreApplication.API.DTOs;
using CoreApplication.API.Helpers;
using CoreApplication.Data.DataEntities;
using CoreApplication.Data.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace CoreApplication.API.Controllers
{
    // uploading the photos functionality for the users
    [Authorize]
    [Route("api/users/{userId}/photos")]
    public class PhotosController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IDatingRepository _userRepo;
        private readonly IOptions<CloudinarySettings> _options;
        private Cloudinary _cloudinary;

        public PhotosController(IMapper mapper, 
                                IDatingRepository userRepo,
                                IOptions<CloudinarySettings> options)
        {
            _options = options;
            _userRepo = userRepo;
            _mapper = mapper;

            var acc = new Account{
                Cloud=_options.Value.CloudName,
                ApiSecret=_options.Value.ApiSecret,
                ApiKey=_options.Value.ApiKey
            };
            
            _cloudinary=new Cloudinary(acc);

        }
      
      [HttpGet("{id}",Name="GetPhoto")]
      public async Task<IActionResult> GetPhoto(int id)
      {
        var photoFromRepo=await _userRepo.GetPhoto(id);
        
        var photo= _mapper.Map<PhotoForReturnDto>(photoFromRepo);
        
        return Ok(photo);
      }



      [HttpPost]
      public async Task<IActionResult> AddPhotoForUser(int userId, PhotoForCreationDto photoDto)
      {
          var user=await _userRepo.GetUser(userId);

          if(user==null)
            return BadRequest("Can not find user");

          var currentUserId= int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

          if(currentUserId!= user.Id)
           return Unauthorized();

          // Now time to upload the photo to cloudinary
            
           var file=photoDto.File;

           var uploadResult= new ImageUploadResult(); 

           if(file.Length>0)
           {
               using(var stream = file.OpenReadStream())
               {
                   var uploadParams= new ImageUploadParams()
                   {
                       File= new FileDescription(file.Name,stream),
                       Transformation = new Transformation().Width(500).Height(500).Crop("fill").Gravity("face")
                   };

                   uploadResult = _cloudinary.Upload(uploadParams);
               }
           }

           photoDto.Url= uploadResult.Uri.ToString();
           photoDto.PublicId=uploadResult.PublicId;

           var photo= _mapper.Map<Photo>(photoDto);
          
           photo.User=user;

           if(!user.Photos.Any(m=>m.IsMain))
           {
               photo.IsMain=true;
           } 

           user.Photos.Add(photo);
           
           var photoReturnDto=_mapper.Map<PhotoForReturnDto>(photo);

           if(await _userRepo.SaveAll())
           {
                return  CreatedAtRoute("GetPhoto",new { id= photo.Id} ,photoReturnDto);
           }

           return BadRequest("Could not upload the photo for some reason");




      }
      //change the isMain property on the photo,this is for the Main btn on the clientside
      [HttpPost("{id}/setMain")]
      public async Task<IActionResult> SetMainPhoto(int userId,int id) 
      {
         // There is an issue with the id itself , if a new photo is uploaded and instantly
         // Main button is clicked then id comes as 0 as it is not yet gets refreshed to get its own id
         // So I am thinking a solution where if id is 0 get the last added photo to be the id
         // so that it can be changed to the latest main photo

         

          if(userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
             return Unauthorized();
          
          if(id==0)
          {
              //this has fixed the issue but a better solution is needed here...
               id=_userRepo.GetLastAddedPhoto(userId);        
          }
          var photoFromRepo = await _userRepo.GetPhoto(id);

          if(photoFromRepo==null) return NotFound();

          if(photoFromRepo.IsMain) return BadRequest("This is already main photo");

          var currentMainPhoto= await _userRepo.GetMainPhoto(userId);

          if(currentMainPhoto!=null)
            currentMainPhoto.IsMain =false;

        photoFromRepo.IsMain=true;

        if(await _userRepo.SaveAll())
           return NoContent();

         return BadRequest("Could not set to main photo");  



      }
    }
}