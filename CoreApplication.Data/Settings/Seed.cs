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

        private readonly UserManager<LoginUser> _userManager;
        public Seed(UserManager<LoginUser> userManager)
        {
            _userManager = userManager;
        }

            
        public void SeedData() {

            if(!_userManager.Users.Any())
            {
                //_userManager.Users.RemoveRange(_userManager.Users);
                //_userManager.SaveChanges();

                var userData = System.IO.File.ReadAllText("./UserSeedData.json");
                var users = JsonConvert.DeserializeObject<List<LoginUser>>(userData);

                foreach (var user in users)
                {
                    //byte[] passwordHash, passwordSalt;

                    //CreatePasswordHash("password", out passwordHash, out passwordSalt);

                    //user.PasswordHash= passwordHash;
                    //user.PasswordSalt= passwordSalt;

                    //  _context.Users.Add(user);
                    _userManager.CreateAsync(user, "password").Wait();
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