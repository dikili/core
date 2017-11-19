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

        private ICoreRepository<Ad> _adRepository;
        private ICoreRepository<Category> _categoryRepository;
        private ICoreRepository<Response> _responseRepository;
        public UnitOfWork(CoreContext ctx, ILogger<CoreRepository<BaseEntity>> logger)
        {
            _coreContext = ctx;
            _logger =logger;
        }
        public ICoreRepository<Ad> AdRepository => _adRepository ?? new CoreRepository<Ad>(_coreContext, _logger);

        public ICoreRepository<Category> CategoryRepository => _categoryRepository ?? new CoreRepository<Category>(_coreContext, _logger);

        public ICoreRepository<Response> ResponseRepository => _responseRepository ?? new CoreRepository<Response>(_coreContext, _logger);

        public void Save()
        {
            _logger.LogInformation("being saved");
            _coreContext.SaveChanges();
        }
    }
}
