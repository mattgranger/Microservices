namespace RabbitMqService.Infrastructure
{
    using System;
    using System.Threading.Tasks;
    using Microservices.EventBus.Abstractions;
    using Microservices.EventBus.Events;

    public class BasicIntegrationEventService : IBasicIntegrationEventService
    {
        private readonly IEventBus eventBus;

        public BasicIntegrationEventService(IEventBus eventBus)
        {
            this.eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
        }

        public async Task PublishThroughEventBusAsync(IntegrationEvent evt)
        {
            this.eventBus.Publish(evt);
            await Task.CompletedTask;
        }
    }
}
