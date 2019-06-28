using System;
using Convey.CQRS.Events;
using Convey.MessageBrokers;

namespace Pacco.Services.Customers.Application.Events.External
{
    [MessageNamespace("identity")]
    public class SignedUp : IEvent
    {
        public Guid Id { get; }
        public string Email { get; }

        public SignedUp(Guid id, string email)
        {
            Id = id;
            Email = email;
        }
    }
}