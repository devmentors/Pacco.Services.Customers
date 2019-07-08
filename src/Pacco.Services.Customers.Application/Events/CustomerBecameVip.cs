using System;
using Convey.CQRS.Events;

namespace Pacco.Services.Customers.Application.Events
{
    [Contract]
    public class CustomerBecameVip : IEvent
    {
        public Guid Id { get; }

        public CustomerBecameVip(Guid id)
        {
            Id = id;
        }
    }
}