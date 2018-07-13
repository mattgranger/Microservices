namespace Mongo.Domain.Events
{
    using System;
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;

    public class BasicEvent
    {
        public BasicEvent()
        {
            this.Id = GenerateId();
        }
        
        [BsonId]
        public string Id { get; set; }

        public Guid EventId { get; set; }

        public DateTime CreationDate { get; set; }

        public string Message { get; set; }

        public static string GenerateId()
        {
            return ObjectId.GenerateNewId().ToString();
        }
    }
}
