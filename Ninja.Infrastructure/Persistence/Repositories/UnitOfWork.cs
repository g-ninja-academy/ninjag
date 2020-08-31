using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Ninja.Application.Common.Interfaces;
using Ninja.Domain.Entities.MongoEntities;
using Ninja.Infrastructure.Persistence.Models.Users;

namespace Ninja.Infrastructure.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public IMongoCollection<MongoUser> users { get; set; }

        public UnitOfWork(IOptions<NinjaDatabaseSettings> settings)
        {
            Users = new Repository<MongoUser>(settings);
        }

        public IRepository<MongoUser> Users { get; }

        IRepository<IMongoUser> IUnitOfWork.Users
        {
            get { return Users; }
        }
    };

    public bool Complete()
    {
    return true;
}

}
}