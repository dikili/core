using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CoreApplication.Data.DataEntities;
using CoreApplication.Data.DataEntities.Interfaces;
using CoreApplication.Data.Repositories.Interfaces;
using Microsoft.Extensions.Logging;

namespace CoreApplication.Data.Repositories
{
    /// <summary>
    /// Deals with entities in MongoDb.
    /// </summary>
    /// <typeparam name="T">The type contained in the repository.</typeparam>
    /// <remarks>Mongo Entities are assumed to use strings for Id's.</remarks>
    public class CoreRepository<T> : ICoreRepository<T> where T : IEntity<int>
    {
        private readonly  CoreContext _coreContext;
        private readonly ILogger<CoreRepository<BaseEntity>> _logger;

        public CoreRepository(CoreContext coreContext,ILogger<CoreRepository<BaseEntity>> logger)
        {
            _coreContext = coreContext;
            _logger = logger;
        }

        public Task AddAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public Task AddAsync(IEnumerable<T> entities)
        {
            throw new NotImplementedException();
        }

        public long Count()
        {
            // throw new NotImplementedException();

            _logger.LogInformation("log something");
            return _coreContext.Trips.Count();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Expression<Func<T, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public bool Exists(Expression<Func<T, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<T> GetAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> GetAsync(Func<T, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(IEnumerable<T> entities)
        {
            throw new NotImplementedException();
        }
    }


}
