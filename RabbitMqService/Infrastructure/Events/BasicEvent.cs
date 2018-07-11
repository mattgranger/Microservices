namespace RabbitMqService.Infrastructure.Events
{
    using Microservices.EventBus.Events;

    public class BasicEvent : IntegrationEvent
    {
        public BasicEvent(string message)
        {
            this.Message = message;
        }

        public string Message { get; set; }
    }
}
