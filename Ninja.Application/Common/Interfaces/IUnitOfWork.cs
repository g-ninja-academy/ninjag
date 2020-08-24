using System;
using System.Collections.Generic;
using System.Text;
using Ninja.Domain.Entities.UserModel;

namespace Ninja.Application.Common.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<User> Users { get; }
        bool Complete();
    }
}