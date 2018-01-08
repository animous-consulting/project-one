using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace PengKep.Common.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        
        IQueryable<TEntity> Get(string includeProperties = "");
        TEntity GetByID(object id);
        void Insert(TEntity entity);
        void Delete(object id);
        void Delete(TEntity entity);
        void Update(TEntity entity);
        void SetAutoDetectChanges(bool b);
    }
}
