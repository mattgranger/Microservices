namespace MongoService.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Threading.Tasks;
    using Core.WebApi.Models;
    using Domain.IntegrationEvents.Events;
    using Microsoft.AspNetCore.Mvc;
    using Mongo.Domain.Events;
    using Mongo.Infrustructure.Repositories;

    [Route("api/v1/[controller]")]
    [ApiController]
    public class BasicEventsController : ControllerBase
    {
        private readonly IMessageingDataRepository repository;

        public BasicEventsController(IMessageingDataRepository repository)
        {
            this.repository = repository;
        }

        // GET api/v1/[controller]/
        [HttpGet]
        [ProducesResponseType(typeof(PagedResultModel<BasicEvent>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(IList<BasicEvent>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Get()
        {
            var basicEvents = await this.repository.GetEvents();
            return this.Ok(basicEvents);
        }

        // GET api/v1/[controller]/id
        [HttpGet]
        [Route("{id:guid}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(BasicEvent),(int)HttpStatusCode.OK)]
        public async Task<IActionResult> Get(Guid id)
        {
            if (id == Guid.Empty)
            {
                return this.BadRequest();
            }

            var basicEvent = await this.GetEventById(id);

            if (basicEvent != null)
            {
                return this.Ok(basicEvent);
            }

            return this.NotFound();
        }

        //POST api/v1/[controller]/
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<IActionResult> CreateProduct([FromBody]BasicIntegrationEvent @event)
        {
            await this.AddOrUpdateBasicEvent(@event);

            return this.CreatedAtAction(nameof(Get), new { id = @event.Id }, null);
        }

        // PUT api/values/5
        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<IActionResult> Put([FromBody]BasicEvent @event)
        {
            await this.repository.Update(@event);

            return this.CreatedAtAction(nameof(Get), new { id = @event.EventId }, null);
        }

        //DELETE api/v1/[controller]/id
        [Route("{id}")]
        [HttpDelete]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var basicEvent = await this.GetEventById(id);

            if (basicEvent == null)
            {
                return this.NotFound();
            }

            await this.repository.DeleteEvent(id);

            return this.NoContent();
        }

        private async Task<BasicEvent> GetEventById(Guid eventId)
        {
            return await this.repository.GetByEventId(eventId);
        }

        private async Task<BasicEvent> AddOrUpdateBasicEvent(BasicIntegrationEvent @event)
        {
            var basicEvent = new BasicEvent
            {
                EventId = @event.Id,
                Message = @event.Message,
                CreationDate = @event.CreationDate
            };

            await this.repository.Insert(basicEvent);

            return basicEvent;
        }
    }
}
