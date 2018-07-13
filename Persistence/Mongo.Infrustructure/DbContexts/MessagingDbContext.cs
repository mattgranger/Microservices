
namespace Mongo.Infrustructure.DbContexts
{
    using Configuration;
    using Domain.Events;
    using MongoDB.Driver;

    public class MessagingDbContext
    {
        private readonly IMongoDatabase database;

        public MessagingDbContext(MongoConfiguration configuration)
        {
            MongoClient client = new MongoClient(configuration.MongoConnectionString);

            this.database = client.GetDatabase(configuration.MongoDatabase);
        }

        public IMongoCollection<BasicEvent> BasicEvents => this.database.GetCollection<BasicEvent>("basic-events");
    }
}
