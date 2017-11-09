using System;
using System.Collections.Generic;
using System.Text;
using CoreApplication.Data.DataEntities.Interfaces;

namespace CoreApplication.Data.Repositories.Interfaces
{
    public interface ICoreRepository<T> : IRepository<T, int> where T : IEntity<int>
    {
    }
}
