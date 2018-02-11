using System.Collections.Generic;
using System.Threading.Tasks;
using CoreApplication.Data.Repositories.Interfaces;
using CoreApplication.Data.DataEntities;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace CoreApplication.Data.Repositories
{
    public class DatingRepository : IDatingRepository
    {
    
       private readonly  CoreContext _coreContext;
        private readonly ILogger<CoreRepository<BaseEntity>> _logger;

        public DatingRepository(CoreContext coreContext,ILogger<CoreRepository<BaseEntity>> logger)
        {
            _coreContext = coreContext;
            _logger = logger;
        }
        public void Add<T>(T entity) where T : class
        {
            _coreContext.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
           _coreContext.Remove(entity);
        }

        public async Task<bool> SaveAll()
        {
           return await _coreContext.SaveChangesAsync() > 0;    
        }

        public async Task<IEnumerable<LoginUser>> GetUsers()
        {
           var users=await _coreContext.LoginUsers.Include(p=>p.Photos).ToListAsync();
           return users;    
        }

        public async Task<LoginUser> GetUser(int id)
        {
            var user=await _coreContext.LoginUsers.Include(p=> p.Photos).FirstOrDefaultAsync(p=> p.Id==id);
            return user;
        }
    }
}