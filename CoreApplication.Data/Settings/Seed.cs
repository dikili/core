using System.Collections.Generic;
using System.Linq;
using CoreApplication.Data.DataEntities;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;

namespace CoreApplication.Data.Settings
{
    public class Seed
    {
        //private readonly CoreContext _context;
        private readonly RoleManager<Role> _roleManager;

        private readonly UserManager<LoginUser> _userManager;
        public Seed(UserManager<LoginUser> userManager, RoleManager<Role> roleManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }


        public void SeedData()
        {

            if (!_userManager.Users.Any())
            {
                //_userManager.Users.RemoveRange(_userManager.Users);
                //_userManager.SaveChanges();

                var userData = System.IO.File.ReadAllText("./UserSeedData.json");
                var users = JsonConvert.DeserializeObject<List<LoginUser>>(userData);


                 var roles = new List<Role>
                 {
                     new Role{Name= "Member"},
                     new Role{Name = "Admin"},
                     new Role {Name = "Moderator"},
                     new Role {Name = "VIP"}
                 };

                 foreach(var role in roles)
                 {
                     _roleManager.CreateAsync(role).Wait();
                 }
                foreach (var user in users)
                {
                    //byte[] passwordHash, passwordSalt;

                    //CreatePasswordHash("password", out passwordHash, out passwordSalt);

                    //user.PasswordHash= passwordHash;
                    //user.PasswordSalt= passwordSalt;

                    //  _context.Users.Add(user);
                    user.Photos.SingleOrDefault().isApproved= true;
                    _userManager.CreateAsync(user, "password").Wait();
                    _userManager.AddToRoleAsync(user,"Member").Wait();
                   
                }
                var adminUser = new LoginUser
                {
                   UserName= "Admin"
                };

                IdentityResult result = _userManager.CreateAsync(adminUser,"password").Result;

                if(result.Succeeded)
                {
                    var admin= _userManager.FindByNameAsync("Admin").Result;
                    _userManager.AddToRolesAsync(admin, new[] {"Admin","Moderator"}).Wait();
               
                }
                //  _context.SaveChanges();
            }

        }

        //   private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        //{
        //   using(var hmac=new System.Security.Cryptography.HMACSHA512())
        //   {
        //       passwordSalt=hmac.Key;
        //       passwordHash=hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        //   }

        //}
    }
}