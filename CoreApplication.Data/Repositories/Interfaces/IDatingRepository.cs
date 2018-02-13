using System.Collections.Generic;
using System.Threading.Tasks;
using CoreApplication.Data.DataEntities;

namespace CoreApplication.Data.Repositories.Interfaces
{
    public interface IDatingRepository
    {
         void Add<T>(T entity) where T: class;

         void Delete<T>(T entity) where T: class;

         bool SaveAll();

         IEnumerable<LoginUser> GetUsers();

         LoginUser GetUser(int id);
    }
}