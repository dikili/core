using CoreApplication.Data.Models;
using CoreApplication.Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreApplication.Data.Uow
{
    public interface IUnitOfWork
    {
       ICoreRepository<Trip> TripRepository { get; }
       ICoreRepository<Stop> StopRepository { get; }

        void Save();
    }
}
