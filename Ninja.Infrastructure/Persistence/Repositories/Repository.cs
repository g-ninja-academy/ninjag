using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Ninja.Application.Common.Interfaces;
using Ninja.Application.Services;
using Ninja.Domain.Common;

namespace Ninja.Infrastructure.Persistence.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly NinjaDatabaseSettings dbSettings;
        private readonly IMongoDatabase _db;
        private readonly IMongoCollection<TEntity> _collection;

        public Repository(IOptions<NinjaDatabaseSettings> settings)
        {
            dbSettings = settings.Value;

            var client = new MongoClient(dbSettings.ConnectionString);
            _db = client.GetDatabase(dbSettings.DatabaseName);

            _collection = _db.GetCollection<TEntity>(GetCollectionName(typeof(TEntity)));
        }

        private protected string GetCollectionName(Type documentType)
        {
            return ((CollectionAttribute) documentType.GetCustomAttributes(
                    typeof(CollectionAttribute),
                    true)
                .FirstOrDefault())?.CollectionName;
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _collection.AsQueryable().AsEnumerable();
        }

        public async Task<TEntity> FindSingle(Expression<Func<TEntity, bool>> predicate)
        {
            return await _collection.Find(predicate).FirstOrDefaultAsync();
        }

        public IEnumerable<TEntity> SearchBy(Expression<Func<TEntity, bool>> predicate)
        {
            return _collection.Find(predicate).ToEnumerable();
        }

        public async Task Add(TEntity entity)
        {
            await _collection.InsertOneAsync(entity);
        }

        public async Task AddRange(IEnumerable<TEntity> entities)
        {
            await _collection.InsertManyAsync(entities);
        }

        public async Task<TEntity> Update(Expression<Func<TEntity, bool>> predicate, TEntity entity)
        {
            return await _collection.FindOneAndReplaceAsync(predicate, entity);
        }

        public async Task Remove(Expression<Func<TEntity, bool>> predicate)
        {
            await _collection.FindOneAndDeleteAsync(predicate);
        }

        public async Task RemoveRange(Expression<Func<TEntity, bool>> predicate)
        {
            await _collection.DeleteManyAsync(predicate);
        }
    }
}