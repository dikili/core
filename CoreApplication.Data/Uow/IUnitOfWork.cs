using CoreApplication.Data.Models;
using CoreApplication.Data.Repositories.Interfaces;


namespace CoreApplication.Data.Uow
{
    public interface IUnitOfWork
    {
      
       ICoreRepository<Trip> TripRepository { get; }
       ICoreRepository<Stop> StopRepository { get; }
  

        void Save();
    }
}
