namespace RabbitMqService.Infrastructure.EventHandlers
{
    using System;
    using System.Threading.Tasks;
    using Events;
    using Microservices.EventBus.Abstractions;
    using Microsoft.Extensions.Logging;

    public class BasicEventHandler: IIntegrationEventHandler<BasicEvent>
    {
        private readonly ILoggerFactory loggerFactory;

        public BasicEventHandler(ILoggerFactory loggerFactory)
        {
            this.loggerFactory = loggerFactory;
        }

        public async Task Handle(BasicEvent @event)
        {
            var logger = this.loggerFactory.CreateLogger(nameof(BasicEventHandler));
            logger.LogInformation($"{Environment.NewLine}Basic integration event has been received and a the message with requestId '{@event.Id}' was '{@event.Message}' {Environment.NewLine}");
            await Task.CompletedTask;
        }
    }
}
