using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using Ninja.Domain.Entities.UserModel;

namespace Ninja.Infrastructure.Persistence.Common
{
    public class UserMap
    {
        public static void Config()
        {
            BsonClassMap.RegisterClassMap<User>(cm =>
            {
                cm.AutoMap();
                cm.MapIdMember(m => m.UserId).SetIdGenerator(CombGuidGenerator.Instance);
            });
        }
    }
}
