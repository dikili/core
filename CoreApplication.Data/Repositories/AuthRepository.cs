using System;
using System.Threading.Tasks;
using CoreApplication.Data.DataEntities;
using CoreApplication.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CoreApplication.Data.Repositories
{
    public class AuthRepository : IAuthRepository
    {
         private readonly  CoreContext _coreContext;
        private readonly ILogger<CoreRepository<BaseEntity>> _logger;
        public AuthRepository(CoreContext coreContext,ILogger<CoreRepository<BaseEntity>> logger)
        {
            _coreContext=coreContext;
            _logger=logger;
        }
        public async Task<LoginUser> Login(string userName, string password)
        {
            
          var user=await _coreContext.Users.Include(p=>p.Photos).FirstOrDefaultAsync(x=>x.UserName==userName);

          if(user==null)
            return null;

          //if(!VerifyPasswordHash(password,user.PasswordHash,user.PasswordSalt))
          //  return null;

            //auth successful

            return user;
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
           using(var hmac=new System.Security.Cryptography.HMACSHA512(passwordSalt))
           {
               var computedHash=hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

               for(int i=0;i<computedHash.Length;i++)
               {
                   if(passwordHash[i]!=computedHash[i])return false;
               }
           }
           return true;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
           using(var hmac=new System.Security.Cryptography.HMACSHA512())
           {
               passwordSalt=hmac.Key;
               passwordHash=hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
           }

        }

        public async Task<LoginUser> Register(LoginUser user, string password)
        {
            byte[] passwordHash,passwordSalt;

            CreatePasswordHash(password,out passwordHash,out passwordSalt);

            //user.PasswordHash=passwordHash;
            //user.PasswordSalt=passwordSalt;

            await _coreContext.Users.AddAsync(user); 
            await  _coreContext.SaveChangesAsync();    

             return user;
        }

        public async Task<bool> UserExists(string username)
        {
           //return await _coreContext.LoginUsers.AnyAsync(x=>x.UserName==username);
           if(await _coreContext.Users.AnyAsync(x=>x.UserName==username))
             return true;

           return false;
        }
    }
}