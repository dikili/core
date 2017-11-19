using CoreApplication.Data.DataEntities;
using CoreApplication.Data.Models;
using CoreApplication.Data.Repositories.Interfaces;


namespace CoreApplication.Data.Uow
{
    public interface IUnitOfWork
    {
      
       ICoreRepository<Ad> AdRepository { get; }
       ICoreRepository<Category> CategoryRepository { get; }
       ICoreRepository<Response> ResponseRepository { get; }

        void Save();
    }
}
