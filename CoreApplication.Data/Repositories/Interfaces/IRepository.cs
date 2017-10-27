using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using CoreApplication.Data.DataEntities;

namespace CoreApplication.Data.Repositories.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        IEnumerable<T> GetAll();
        T Get(long id);
        void Insert(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
