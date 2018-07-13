namespace Domain.IntegrationEvents.Events
{
    using Microservices.EventBus.Events;

    public class PersistedBasicIntegrationEvent : IntegrationEvent
    {
        public PersistedBasicIntegrationEvent(string message)
        {
            this.Message = message;
        }

        public string Message { get; set; }
    }
}
