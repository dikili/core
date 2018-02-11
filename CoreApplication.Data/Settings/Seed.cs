using System.Collections.Generic;
using CoreApplication.Data.DataEntities;
using Newtonsoft.Json;

namespace CoreApplication.Data.Settings
{
    public class Seed
    {
        private readonly CoreContext _context;

        public Seed(CoreContext context)
        {
            _context=context;
        }

         public void SeedData() {

             _context.LoginUsers.RemoveRange(_context.LoginUsers);
             _context.SaveChanges();

             var userData= System.IO.File.ReadAllText("./UserSeedData.json");
             var users=JsonConvert.DeserializeObject<List<LoginUser>>(userData);

             foreach(var user in users)
             {
                 byte[] passwordHash, passwordSalt;

                 CreatePasswordHash("password",out passwordHash ,out passwordSalt);

                 user.PasswordHash= passwordHash;
                 user.PasswordSalt= passwordSalt;

                 _context.LoginUsers.Add(user);

             }

             _context.SaveChanges();
         }

           private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
           using(var hmac=new System.Security.Cryptography.HMACSHA512())
           {
               passwordSalt=hmac.Key;
               passwordHash=hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
           }

        }
    }
}