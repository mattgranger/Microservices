namespace Mongo.Infrustructure.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Configuration;
    using DbContexts;
    using Domain.Events;
    using Microsoft.Extensions.Options;
    using MongoDB.Driver;

    public class MessageingDataRepository
        : IMessageingDataRepository
    {
        private readonly MessagingDbContext context;

        public MessageingDataRepository(IOptions<MongoConfiguration> settings)
        {
            this.context = new MessagingDbContext(settings.Value);
        }

        public async Task<IList<BasicEvent>> GetEvents()
        {
            var basicEvents = await this.context.BasicEvents.FindAsync(Builders<BasicEvent>.Filter.Empty);
            return await basicEvents.ToListAsync();
        }

        public async Task<BasicEvent> GetByEventId(Guid id)
        {
            var filter = Builders<BasicEvent>.Filter.Eq("EventId", id);
            return await this.context.BasicEvents
                .Find(filter)
                .FirstOrDefaultAsync();
        }

        public async Task Insert(BasicEvent @event)
        {
            await this.context.BasicEvents.InsertOneAsync(@event);
        }

        public async Task Update(BasicEvent @event)
        {
            await this.context.BasicEvents.ReplaceOneAsync(e => e.EventId == @event.EventId, @event);
        }

        public async Task DeleteEvent(Guid id)
        {
            var filter = Builders<BasicEvent>.Filter.Eq("EventId", id);
            await this.context.BasicEvents.DeleteOneAsync(filter);
        }
    }
}
