using System.Collections.Generic;
using System.Threading.Tasks;
using CoreApplication.Data.DataEntities;

namespace CoreApplication.Data.Repositories.Interfaces
{
    public interface IDatingRepository
    {
         void Add<T>(T entity) where T: class;

         void Delete<T>(T entity) where T: class;

         //bool SaveAll();
        Task<bool> SaveAll();
         IEnumerable<LoginUser> GetUsers();
        // Task<IEnumerable<LoginUser>> GetUsers();

        // LoginUser GetUser(int id);
         Task<LoginUser> GetUser(int id);
        // Photo GetPhoto(int id);

        Task<Photo> GetPhoto(int id);

        Task<Photo> GetMainPhoto(int userId);

        int GetLastAddedPhoto(int userId);
    }
}