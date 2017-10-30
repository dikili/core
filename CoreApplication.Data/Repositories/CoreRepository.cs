using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CoreApplication.Data.DataEntities;
using CoreApplication.Data.DataEntities.Interfaces;
using CoreApplication.Data.Repositories.Interfaces;
using CoreApplication.Data.Settings;
using Microsoft.Extensions.Options;

namespace CoreApplication.Data.Repositories
{
    /// <summary>
    /// Deals with entities in MongoDb.
    /// </summary>
    /// <typeparam name="T">The type contained in the repository.</typeparam>
    /// <remarks>Mongo Entities are assumed to use strings for Id's.</remarks>
    public class CoreRepository<T> : IRepository<T, int> where T : IEntity<int>
    {
       
    }


}
