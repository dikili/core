using System.Collections.Generic;
using System.Threading.Tasks;
using CoreApplication.Data.DataEntities;
using CoreApplication.Data.Helpers;

namespace CoreApplication.Data.Repositories.Interfaces
{
    public interface IDatingRepository
    {
         void Add<T>(T entity) where T: class;

         void Delete<T>(T entity) where T: class;

         //bool SaveAll();
        Task<bool> SaveAll();
        // IEnumerable<LoginUser> GetUsers();
         Task<PagedList<LoginUser>> GetUsers(UserParams userParams);

         Task<Like> GetLike(int userId,int recepientId);

        // PagedList<LoginUser> GetUsers();
        // LoginUser GetUser(int id);
        Task<LoginUser> GetUser(int id, bool isCurrentUser);
        // Photo GetPhoto(int id);

        Task<Photo> GetPhoto(int id);

        Task<Photo> GetMainPhoto(int userId);

        int GetLastAddedPhoto(int userId);

        Task<Message> GetMessage(int id);

        Task<PagedList<Message>> GetMessagesForUser(MessageParams messageParams);
    
        Task<IEnumerable<Message>> GetMessageThread(int userId, int recepientId);

        bool AreThereUnreadMessages(int userId);
    }
}