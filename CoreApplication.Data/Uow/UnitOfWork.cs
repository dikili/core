using CoreApplication.Data.Models;
using CoreApplication.Data.Repositories.Interfaces;
using CoreApplication.Data.Repositories;
using Microsoft.Extensions.Logging;
using CoreApplication.Data.DataEntities;

namespace CoreApplication.Data.Uow
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CoreContext _coreContext;
        public readonly ILogger<CoreRepository<BaseEntity>> _logger;

        private ICoreRepository<Trip> _tripRepository;
        private ICoreRepository<Stop> _stopRepository;
        public UnitOfWork(CoreContext ctx, ILogger<CoreRepository<BaseEntity>> logger)
        {
            _coreContext = ctx;
            _logger =logger;
        }
        public ICoreRepository<Trip> TripRepository => _tripRepository ?? new CoreRepository<Trip>(_coreContext, _logger);

        public ICoreRepository<Stop> StopRepository => _stopRepository ?? new CoreRepository<Stop>(_coreContext, _logger);

        public void Save()
        {
            _logger.LogInformation("being saved");
            _coreContext.SaveChanges();
        }
    }
}
