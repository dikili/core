using System;
using System.Collections.Generic;
using System.Text;
using CoreApplication.Data.Models;
using CoreApplication.Data.Repositories.Interfaces;
using CoreApplication.Data.Repositories;

namespace CoreApplication.Data.Uow
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CoreContext _coreContext;
        private ICoreRepository<Trip> _tripRepository;
        private ICoreRepository<Stop> _stopRepository;
        public UnitOfWork(CoreContext ctx)
        {
            _coreContext = ctx;
        }

        public ICoreRepository<Trip> TripRepository => _tripRepository ?? new CoreRepository<Trip>(_coreContext);

        public ICoreRepository<Stop> StopRepository => _stopRepository ?? new CoreRepository<Stop>(_coreContext);

        public void Save()
        {
            _coreContext.SaveChanges();
        }
    }
}
