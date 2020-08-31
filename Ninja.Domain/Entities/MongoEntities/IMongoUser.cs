using System;
using System.Collections.Generic;
using System.Text;

namespace Ninja.Domain.Entities.MongoEntities
{
    public interface IMongoUser
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
    }
}
