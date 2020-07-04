using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace URLCatalogue.API.Model.Data
{
    public class User: MongoDbModel
    {
        [BsonId]
        public ObjectId _id { get; set; }
        [BsonElement("id")]
        public Guid Id { get; set; }
        [BsonElement("isActive")]
        public bool IsActive { get; set; }
        [BsonElement("email")]
        public string Email { get; set; }
    }
}
