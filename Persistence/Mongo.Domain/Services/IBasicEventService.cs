namespace Mongo.Domain.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Events;

    public interface IBasicEventService
    {
        Task<IList<BasicEvent>> GetEvents();

        Task<BasicEvent> GetByEventId(Guid id);

        Task Insert(BasicEvent @event);

        Task Update(BasicEvent @event);

        Task DeleteEvent(Guid id);
    }
}
