using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Ninja.Application.Common.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> GetAll();
        TEntity FindSingle(Predicate<TEntity> predicate);
        IEnumerable<TEntity> SearchBy(Expression<Func<TEntity, bool>> predicate);

        void Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);

        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entities);
    }
}