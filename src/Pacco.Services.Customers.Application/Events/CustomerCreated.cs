using System;
using Convey.CQRS.Events;

namespace Pacco.Services.Customers.Application.Events
{
    [Contract]
    public class CustomerCreated : IEvent
    {
        public Guid Id { get; }

        public CustomerCreated(Guid id)
        {
            Id = id;
        }
    }
}