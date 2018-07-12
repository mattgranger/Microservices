namespace RabbitMqService
{
    public class QueueSettings
    {
        public string BasicQueueName { get; set; }

        public string EventBusConnection { get; set; }

        public bool UseCustomisationData { get; set; }

        public bool AzureStorageEnabled { get; set; }
    }
}
