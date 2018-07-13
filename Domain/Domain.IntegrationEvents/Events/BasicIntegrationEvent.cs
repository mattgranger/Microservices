namespace Domain.IntegrationEvents.Events
{
    using Microservices.EventBus.Events;

    public class BasicIntegrationEvent : IntegrationEvent
    {
        public BasicIntegrationEvent(string message)
        {
            this.Message = message;
        }

        public string Message { get; set; }
    }
}
