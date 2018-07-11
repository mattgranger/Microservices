namespace RabbitMqService.Controllers
{
    using System;
    using System.Text;
    using System.Threading.Tasks;
    using Infrastructure;
    using Infrastructure.Events;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using RabbitMQ.Client;

    [Route("api/v1/[controller]")]
    [ApiController]
    public class HelloWorldController : Controller
    {
        private readonly IBasicIntegrationEventService basicIntegrationEventService;
        private readonly ILogger<HelloWorldController> logger;
        private readonly QueueSettings settings;

        public HelloWorldController(IBasicIntegrationEventService basicIntegrationEventService, ILogger<HelloWorldController> logger, IOptions<QueueSettings> settings)
        {
            this.basicIntegrationEventService = basicIntegrationEventService;
            this.logger = logger;
            this.settings = settings?.Value ?? throw new ArgumentNullException(nameof(settings));
        }

        [HttpGet]
        [Route("publish")]
        public async Task<IActionResult> BasicPublish(string message)
        {
            await this.basicIntegrationEventService.PublishThroughEventBusAsync(new BasicEvent(message));

            return new OkResult();
        }
    }
}