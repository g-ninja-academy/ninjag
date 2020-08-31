using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Ninja.Domain.Entities.MongoEntities;
using Ninja.Infrastructure.Persistence.Common;

namespace Ninja.Infrastructure.Persistence.Models.Users
{
    [BsonCollection("MongoUser")]
    public class MongoUser : IMongoUser
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public int Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
    }
}
