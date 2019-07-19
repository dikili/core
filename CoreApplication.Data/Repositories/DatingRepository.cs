using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreApplication.Data.Repositories.Interfaces;
using CoreApplication.Data.DataEntities;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System;
using CoreApplication.Data.Helpers;

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

        public async Task<bool> SaveAll()
        {
         
            var updated=await _coreContext.SaveChangesAsync();
            return updated > 0;
        }

        public async Task<PagedList<LoginUser>> GetUsers(UserParams userParams)
        {
            // using(var ctx=_fact.CreateDbContext(new string[] {}))
            // {
            //      return  ctx.LoginUsers.Include(p=>p.Photos);   
            // }
           var users= _coreContext.Users.Include(p=>p.Photos).OrderByDescending(x=>x.LastActive).AsQueryable();

           users = users.Where(x=>x.Id!= userParams.UserId);
          if(!string.IsNullOrEmpty(userParams.Building))
          users = users.Where(x=>x.busCategory == userParams.Building);
        //  if(userParams.Likers)
        //  {
        //      var x =new List<LoginUser>();
        //      var userLikers= await GetUserLikes(userParams.UserId,userParams.Likers);
        //      foreach(var user in users)
        //      {
                 
        //         foreach(var likers in userLikers)
        //         {
        //             if(user.Id==likers.LikerId)
        //             {
        //                 x.Add(user);
        //             }
        //         }
        //      }
        //      users=x.AsQueryable();
        //  }
        //    if(userParams.Likees)
        //  {
        //      var x =new List<LoginUser>();
        //      var userLikees= await GetUserLikes(userParams.UserId,userParams.Likers);
        //      foreach(var user in users)
        //      {
                 
        //         foreach(var likee in userLikees)
        //         {
        //             if(user.Id==likee.LikeeId)
        //             {
        //                 x.Add(user);
        //             }
        //         }
        //      }
        //      users=x.AsQueryable();
        //  }
            // if(userParams.Likers)
            // {
            //     var userLikers= await GetUserLikes(userParams.UserId,userParams.Likers);
            //     users= users.Where(u=>userLikers.Any(likers=>likers.LikerId==u.Id));
            // }

            // if(userParams.Likees)
            // {
            //     var userLikees= await GetUserLikes(userParams.UserId,userParams.Likers);
            //     users= users.Where(u=>userLikees.Any(likees=>likees.LikeeId==u.Id));
            // }


           if(userParams.MinAge != 18 || userParams.MaxAge != 99) {
            //    users = users.Where(x=> CalculateAge(x.DateOfBirth) >= userParams.MinAge 
            //    && CalculateAge(x.DateOfBirth) <= userParams.MaxAge);

               var min=DateTime.Today.AddYears(-userParams.MaxAge -1);
               var max=DateTime.Today.AddYears(-userParams.MinAge);

               users = users.Where(u=> u.DateOfBirth >= min && u.DateOfBirth <= max);
           }

           if (!string.IsNullOrEmpty(userParams.OrderBy))
           {
               switch(userParams.OrderBy)
               {
                   case "created":
                     users= users.OrderByDescending(x=>x.Created);
                     break;
                   default :
                      users.OrderByDescending(x=>x.LastActive);
                      break;  
               }
           }

           

           return PagedList<LoginUser>.Create(users,userParams.PageNumber,userParams.PageSize);
        }

        public async Task<LoginUser> GetUser(int id, bool isCurrentUser)
        {
            //context is correctly injected by the DI but somehow async methods seem to have
            // issues so got rid of those
            var query = _coreContext.Users.Include(p => p.Photos).AsQueryable();

            if (isCurrentUser)
                query = query.IgnoreQueryFilters();

            var user = await query.FirstOrDefaultAsync(u => u.Id == id);

            return user;

           //return
           //     await _coreContext.Users.Include(p => p.Photos)
           //       .FirstOrDefaultAsync(p => p.Id == id); //await ctx.LoginUsers.Include(p=>p.Photos).FirstOrDefaultAsync(x=>x.Id==id); //.Find(id); //.Where(p=> p.Id == id);


        }
        public List<Photo> GetPhotos() {
            return _coreContext.Photos.Where(p => p.isApproved == false).ToList();
        }
        public async Task<Photo> GetPhoto(int id)
        {
            return await _coreContext.Photos.IgnoreQueryFilters()
                .FirstOrDefaultAsync(p=>p.Id==id);
        }

        public async Task<Photo> GetMainPhoto(int userId)
        {
          return await  _coreContext.Photos.Where(p=>p.LoginUserId==userId).FirstOrDefaultAsync(p=>p.IsMain);
        }

        public int GetLastAddedPhoto(int userId)
        {
            return  _coreContext.Photos.Where(p=>p.LoginUserId==userId).LastOrDefault().Id;
        }

           private  int CalculateAge(DateTime theDateTime)
        {
            var age= DateTime.Today.Year - theDateTime.Year;

            if(theDateTime.AddYears(age) > DateTime.Today)
                    age--;

            return age;
        }

        public async Task<Like> GetLike(int userId, int recepientId)
        {
            return await _coreContext.Likes.FirstOrDefaultAsync(x=>x.LikerId==userId && x.LikeeId==recepientId);
        }

          private async Task<IEnumerable<Like>> GetUserLikes(int id, bool likers)
        {
            var user = await _coreContext.Users
            .Include(x=>x.Likee)
            .Include(x=>x.Liker)
            .FirstOrDefaultAsync(u=>u.Id==id);

            var likee = user.Likee;
            var liker = user.Liker;
            if(likers)
            {   var x=likee.Where(u=>u.LikeeId==id).ToList();
                return x;//user.Likee.Where(u=>u.LikeeId==id).ToList();
            }
            else{
                 var y=liker.Where(u=>u.LikerId==id).ToList();
                return y;
            }
        }

        public async Task<Message> GetMessage(int id)
        {
            return await _coreContext.Messages.FirstOrDefaultAsync(m=>m.Id==id);
        }

        public async Task<IEnumerable<Message>> GetMessageThread(int userId, int recepientId)
        {
            var messageList = _coreContext.Messages.Include(u=>u.Receiver).ThenInclude(p=>p.Photos)
                        .Include(x=>x.Sender).ThenInclude(z=>z.Photos);
            var messages=await messageList
                        .Where(m=>(m.ReceiverId==userId && m.ReciepentDeleted==false && m.SenderId==recepientId)
                               || (m.ReceiverId==recepientId && m.SenderDeleted==false  && m.SenderId==userId))
                               .OrderByDescending(x=>x.MessageSent)
                               .ToListAsync();

              return messages;                 
        }
        
        public bool AreThereUnreadMessages(int userId)
        {
           var messages=_coreContext.Messages
                        .Include(u=>u.Receiver)
                        .AsQueryable();

           return  messages.Where(u=>u.ReceiverId==userId && u.ReciepentDeleted == false && u.IsRead == false).Count()>0;
  
        }
        public async Task<PagedList<Message>> GetMessagesForUser(MessageParams messageParams)
        {
           var messages=_coreContext.Messages
                        .Include(u=>u.Receiver).ThenInclude(p=>p.Photos)
                        .Include(x=>x.Sender).ThenInclude(z=>z.Photos)
                        .AsQueryable();


             switch (messageParams.MessageContainer)
             {
                 case "Inbox":
                    messages=messages.Where(u=>u.ReceiverId==messageParams.UserId && u.ReciepentDeleted == false);
                    break;
                 case "Outbox" :
                    messages= messages.Where(u=>u.SenderId==messageParams.UserId && u.SenderDeleted == false);
                    break;
                  default : 
                    messages=messages.Where(u=>u.ReceiverId==messageParams.UserId && u.ReciepentDeleted == false && u.IsRead==false);
                    break;
             }
               
            messages=messages.OrderByDescending(d=>d.MessageSent);

            return await PagedList<Message>.CreateAsycn(messages,messageParams.PageNumber,messageParams.PageSize);



        }
    }
}