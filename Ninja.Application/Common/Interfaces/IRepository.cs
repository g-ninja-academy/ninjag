using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Ninja.Application.Common.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> GetAll();
        Task<TEntity> FindSingle(Expression<Func<TEntity, bool>> predicate);
        IEnumerable<TEntity> SearchBy(Expression<Func<TEntity, bool>> predicate);

        Task Add(TEntity entity);
        Task AddRange(IEnumerable<TEntity> entities);

        Task Remove(Expression<Func<TEntity, bool>> predicate);
        Task RemoveRange(Expression<Func<TEntity, bool>> predicate);
    }
}