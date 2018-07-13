namespace Domain.IntegrationEvents.Settings
{
    public class QueueSettings
    {
        public string SubscriptionClientName { get; set; }

        public string EventBusConnection { get; set; }

        public bool UseCustomisationData { get; set; }

        public bool AzureStorageEnabled { get; set; }
    }
}
