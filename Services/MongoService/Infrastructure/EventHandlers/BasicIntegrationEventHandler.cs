namespace MongoService.Infrastructure.EventHandlers
{
    using System;
    using System.Threading.Tasks;
    using Domain.IntegrationEvents.Events;
    using Microservices.EventBus.Abstractions;
    using Microsoft.Extensions.Logging;
    using Mongo.Domain.Events;
    using Mongo.Infrustructure.Repositories;

    public class BasicIntegrationEventHandler: IIntegrationEventHandler<BasicIntegrationEvent>
    {
        private readonly ILoggerFactory loggerFactory;
        private readonly IMessageingDataRepository repository;

        public BasicIntegrationEventHandler(ILoggerFactory loggerFactory, IMessageingDataRepository repository)
        {
            this.loggerFactory = loggerFactory;
            this.repository = repository;
        }

        public async Task Handle(BasicIntegrationEvent @event)
        {
            var logger = this.loggerFactory.CreateLogger(nameof(BasicIntegrationEvent));
            logger.LogInformation($"{Environment.NewLine}{Environment.NewLine}MONGOSERVICE:: Persisted Basic integration event has been received and a the message with requestId '{@event.Id}' was '{@event.Message}' {Environment.NewLine}");

            await this.repository.Insert(new BasicEvent
            {
                Message = @event.Message,
                EventId = @event.Id,
                CreationDate = @event.CreationDate
            });
        }
    }
}
