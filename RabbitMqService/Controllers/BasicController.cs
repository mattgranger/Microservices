namespace RabbitMqService.Controllers
{
    using System;
    using System.Threading.Tasks;
    using Domain.IntegrationEvents.Events;
    using Domain.IntegrationEvents.Settings;
    using Microservices.EventBus.Abstractions;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;

    [Route("api/v1/[controller]")]
    [ApiController]
    public class BasicController : Controller
    {
        private readonly IEventBus eventBus;
        private readonly ILogger<BasicController> logger;
        private readonly QueueSettings settings;

        public BasicController(IEventBus eventBus, ILogger<BasicController> logger, IOptions<QueueSettings> settings)
        {
            this.eventBus = eventBus;
            this.logger = logger;
            this.settings = settings?.Value ?? throw new ArgumentNullException(nameof(settings));
        }

        [HttpGet]
        [Route("publish")]
        public async Task<IActionResult> BasicPublish(string message)
        {
            this.eventBus.Publish(new BasicIntegrationEvent(message));
            this.eventBus.Publish(new PersistedBasicIntegrationEvent(message));
            this.logger.LogError($"{Environment.NewLine}The message: '{message}' was published {Environment.NewLine}");
            await Task.CompletedTask;
            
            return new OkResult();
        }
    }
}