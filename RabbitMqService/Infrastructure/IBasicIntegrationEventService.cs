namespace RabbitMqService.Infrastructure
{
    using System.Threading.Tasks;
    using Microservices.EventBus.Events;

    public interface IBasicIntegrationEventService
    {
        Task PublishThroughEventBusAsync(IntegrationEvent evt);
    }
}
