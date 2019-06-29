using System;
using Convey.CQRS.Events;

namespace Pacco.Services.Customers.Application.Events.Rejected
{
    [Contract]
    public class CreateCustomerRejected : IRejectedEvent
    {
        public Guid Id { get; }
        public string Reason { get; }
        public string Code { get; }

        public CreateCustomerRejected(Guid id, string reason, string code)
        {
            Id = id;
            Reason = reason;
            Code = code;
        }
    }
}