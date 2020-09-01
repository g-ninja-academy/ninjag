using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.VisualBasic.CompilerServices;
using MongoDB.Driver;
using Ninja.Application.Common.Interfaces;
using Ninja.Domain.Entities.UserModel;

namespace Ninja.Infrastructure.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public IMongoCollection<User> users { get; set; }

        public UnitOfWork(IOptions<NinjaDatabaseSettings> settings)
        {
            Users = new Repository<User>(settings);
        }

        public IRepository<User> Users { get; }
    }
}