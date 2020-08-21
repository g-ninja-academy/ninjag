using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Ninja.Application.Common.Interfaces;
using Ninja.Application.Services;

namespace Ninja.Infrastructure.Persistence.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        //private readonly UsersService _service;
        private List<TEntity> DbSet { get; set; }

        public Repository(List<TEntity> entities)
        {
            DbSet = entities;
        }

        public IEnumerable<TEntity> GetAll()
        {
            return DbSet;
        }

        public TEntity FindSingle(Predicate<TEntity> predicate)
        {
            return DbSet.Find(predicate);
        }

        public IEnumerable<TEntity> SearchBy(Expression<Func<TEntity, bool>> predicate)
        {
            return DbSet.AsQueryable().Where(predicate).AsEnumerable();
        }

        public void Add(TEntity entity)
        {
            DbSet.Add(entity);
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            DbSet.AddRange(entities);
        }

        public void Remove(TEntity entity)
        {
            DbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                DbSet.Remove(entity);
            }
        }
    }
}