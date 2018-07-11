namespace RabbitMqService.Infrastructure.EventHandlers
{
    using System.Threading.Tasks;
    using Events;
    using Microservices.EventBus.Abstractions;
    using Microsoft.Extensions.Logging;

    public class BasicEventHandler: IIntegrationEventHandler<BasicEvent>
    {
        private readonly ILoggerFactory logger;

        public BasicEventHandler(ILoggerFactory logger)
        {
            this.logger = logger;
        }

        public async Task Handle(BasicEvent @event)
        {
            this.logger.CreateLogger(nameof(BasicEventHandler))
                .LogTrace($"UserCheckoutAccepted integration event has been received and a create new order process is started with requestId: {@event.Id}");

            await Task.CompletedTask;
        }
    }
}
