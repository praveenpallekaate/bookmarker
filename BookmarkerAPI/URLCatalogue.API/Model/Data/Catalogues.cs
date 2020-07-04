using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace URLCatalogue.API.Model.Data
{
    public class Catalogues : MongoDbModel
    {
        [BsonId]
        public ObjectId _id { get; set; }
        [BsonElement("id")]
        public Guid Id { get; set; }
        [BsonElement("isActive")]
        public bool IsActive { get; set; }
        [BsonElement("type")]
        public string Type { get; set; }
        [BsonElement("catalogues")]
        public IEnumerable<Catalogue> CatalogueList { get; set; }
    }

    public class Catalogue
    {
        [BsonElement("id")]
        public Guid Id { get; set; }
        [BsonElement("name")]
        public string Name { get; set; }
        [BsonElement("details")]
        public string Details { get; set; }
        [BsonElement("path")]
        public string Path { get; set; }
        [BsonElement("tags")]
        public string Tags { get; set; }
        [BsonElement("category")]
        public string Category { get; set; }
        [BsonElement("tileImageUrl")]
        public string TileImageUrl { get; set; }
        [BsonElement("isActive")]
        public bool IsActive { get; set; }
    }
}
