namespace RabbitMqService.Infrastructure.EventHandlers
{
    using System;
    using System.Threading.Tasks;
    using Domain.IntegrationEvents.Events;
    using Microservices.EventBus.Abstractions;
    using Microsoft.Extensions.Logging;

    public class BasicIntegrationEventHandler: IIntegrationEventHandler<BasicIntegrationEvent>
    {
        private readonly ILoggerFactory loggerFactory;

        public BasicIntegrationEventHandler(ILoggerFactory loggerFactory)
        {
            this.loggerFactory = loggerFactory;
        }

        public async Task Handle(BasicIntegrationEvent @event)
        {
            var logger = this.loggerFactory.CreateLogger(nameof(BasicIntegrationEventHandler));
            logger.LogInformation($"{Environment.NewLine}{Environment.NewLine}RABBITMQSERVICE:: Basic integration event has been received and a the message with requestId '{@event.Id}' was '{@event.Message}' {Environment.NewLine}");
            await Task.CompletedTask;
        }
    }
}
