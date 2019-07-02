using System.Collections.Generic;
using System.Linq;
using Convey.CQRS.Events;
using Pacco.Services.Customers.Application.Services;
using Pacco.Services.Customers.Core;

namespace Pacco.Services.Customers.Infrastructure.Services
{
    public class EventMapper : IEventMapper
    {
        public IEnumerable<IEvent> MapAll(IEnumerable<IDomainEvent> events)
            => events.Select(Map);

        public IEvent Map(IDomainEvent @event)
        {
//            switch (@event)
//            {
//            }

            return null;
        }
    }
}