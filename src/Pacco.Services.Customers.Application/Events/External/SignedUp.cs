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
        public string Role { get; }
        
        public SignedUp(Guid id, string email, string role)
        {
            Id = id;
            Email = email;
            Role = role;
        }
    }
}