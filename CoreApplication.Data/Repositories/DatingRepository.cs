using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreApplication.Data.Repositories.Interfaces;
using CoreApplication.Data.DataEntities;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace CoreApplication.Data.Repositories
{
    public class DatingRepository : IDatingRepository
    {
      // private readonly DesignTimeDbContextFactory _fact;
       private readonly  CoreContext _coreContext;
        private readonly ILogger<CoreRepository<BaseEntity>> _logger;

        public DatingRepository(CoreContext context,ILogger<CoreRepository<BaseEntity>> logger)
        {
            // _fact=new DesignTimeDbContextFactory();
            _coreContext = context;
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

        public bool SaveAll()
        {
         
            var updated=_coreContext.SaveChanges();
            return updated > 0;
        }

        public IEnumerable<LoginUser> GetUsers()
        {
            // using(var ctx=_fact.CreateDbContext(new string[] {}))
            // {
            //      return  ctx.LoginUsers.Include(p=>p.Photos);   
            // }
           return _coreContext.LoginUsers.Include(p=>p.Photos);
        }

        public LoginUser GetUser(int id)
        {  
             //context is correctly injected by the DI but somehow async methods seem to have
             // issues so got rid of those
            
           return
                _coreContext.LoginUsers.Include(p => p.Photos)
                  .FirstOrDefault(p=>p.Id==id); //await ctx.LoginUsers.Include(p=>p.Photos).FirstOrDefaultAsync(x=>x.Id==id); //.Find(id); //.Where(p=> p.Id == id);


        }
    }
}