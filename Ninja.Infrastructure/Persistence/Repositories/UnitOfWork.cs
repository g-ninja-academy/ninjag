using System;
using System.Collections.Generic;
using System.Text;
using Ninja.Application.Common.Interfaces;
using Ninja.Domain.Entities.UserModel;

namespace Ninja.Infrastructure.Persistence.Repositories
{
    class UnitOfWork : IUnitOfWork
    {
        public List<User> users { get; set; }

        public UnitOfWork()
        {
            users = new List<User>();
            Users = new Repository<User>(users);
        }

        public IRepository<User> Users { get; }

        public bool Complete()
        {
            return true;
        }
    }
}