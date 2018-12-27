using CoreApplication.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using CoreApplication.API.DTOs;
using Microsoft.AspNetCore.Identity;
using CoreApplication.Data.DataEntities;
using Microsoft.Extensions.Options;
using CoreApplication.API.Helpers;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

namespace CoreApplication.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdminController : ControllerBase
    {
        private CoreContext _context;
        private Cloudinary _cloudinary;
        IOptions<CloudinarySettings> _cloudinaryConfig;
        private readonly UserManager<LoginUser> _userManager;

        public AdminController(CoreContext context,
            UserManager<LoginUser> userManager,
            IOptions<CloudinarySettings> cloudinaryConfig)
        {
            _context = context;
            _userManager = userManager;
            _cloudinaryConfig = cloudinaryConfig;

            Account acc = new Account(_cloudinaryConfig.Value.CloudName,
                _cloudinaryConfig.Value.ApiKey,
                _cloudinaryConfig.Value.ApiSecret);

            _cloudinary = new Cloudinary(acc);
        }
        [Authorize(Policy="RequireAdminRole")]
        [HttpGet("usersWithRoles")]
        public async Task<IActionResult> GetUsersWithRoles()
        {
            var userList = await (from user in _context.Users
                                  orderby user.UserName
                                  select new
                                 {
                                     Id = user.Id,
                                     UserName = user.UserName,
                                     Roles = (from userRole in user.UserRoles
                                              join role in _context.Roles
                                              on userRole.RoleId
                                              equals role.Id
                                              select role.Name).ToList()
                                 }).ToListAsync();
                                 
            return Ok(userList);
        }
        [Authorize(Policy = "RequireAdminRole")]
        [HttpPost("editRoles/{userName}")]
        public async Task<IActionResult> EditRoles(string userName, RoleEditDto roleEditDto)
        {
            var user = await _userManager.FindByNameAsync(userName);

            var userRoles = await _userManager.GetRolesAsync(user);

            var selectedRoles = roleEditDto.RoleNames;

            selectedRoles = selectedRoles ?? new string[] { };

            var result = await _userManager.AddToRolesAsync(user, selectedRoles.Except(userRoles));

            if (!result.Succeeded)
                return BadRequest("Failed to add roles");

            result = await _userManager.RemoveFromRolesAsync(user, userRoles.Except(selectedRoles));

            if (!result.Succeeded)
                return BadRequest("Failed to remove the roles");

            return Ok(await _userManager.GetRolesAsync(user));
        }

        [Authorize(Policy = "ModeratePhotoRole")]
        [HttpGet("photosForModeration")]
        public async Task<IActionResult> GetPhotosForModeration()
        {
            var photos = await _context.Photos
                .Include(u => u.User)
                .IgnoreQueryFilters()
                .Where(p => p.isApproved == false)
                .Select(u => new
                {
                    Id = u.Id,
                    UserName = u.User.UserName,
                    Url = u.Url,
                    isApproved = u.isApproved
                }).ToListAsync();

            //return Ok("Admins or moderators can see this");

            return Ok(photos);
        }
        [Authorize(Policy = "ModeratePhotoRole")]
        [HttpPost("approvePhoto/{photoId}")]
        public async Task<IActionResult> ApprovePhoto(int photoId)
        {
            var photo = await _context.Photos
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(p => p.Id == photoId);

            photo.isApproved = true;

            await _context.SaveChangesAsync();

            return Ok();

        }


        [Authorize(Policy = "ModeratePhotoRole")]
        [HttpPost("rejectPhoto/{photoId}")]
        public async Task<IActionResult> RejectPhoto(int photoId)
        {
            var photo = await _context.Photos
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(p => p.Id == photoId);

            if (photo.IsMain)
                return BadRequest("You cannot reject the main photo");

            if(photo.PublicId !=null)
            {
                var deleteParams = new DeletionParams(photo.PublicId);

                var result = _cloudinary.Destroy(deleteParams);

                if(result.Result == "ok")
                {
                    _context.Photos.Remove(photo);
                }
            }
           
            if(photo.PublicId == null)
            {
                _context.Photos.Remove(photo);
            }
            await _context.SaveChangesAsync();

            return Ok();

        }


    }
}