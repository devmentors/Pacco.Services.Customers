using System;
using Convey.CQRS.Events;
using Convey.MessageBrokers;

namespace Pacco.Services.Customers.Application.Events.External
{
    [MessageNamespace("orders")]
    public class OrderCompleted : IEvent
    {
        public Guid OrderId { get; }
        public Guid CustomerId { get; }

        public OrderCompleted(Guid orderId, Guid customerId)
        {
            OrderId = orderId;
            CustomerId = customerId;
        }
    }
}