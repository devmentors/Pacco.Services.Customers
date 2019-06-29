using System;
using Convey.CQRS.Commands;

namespace Pacco.Services.Customers.Application.Commands
{
    [Contract]
    public class CreateCustomer : ICommand
    {
        public Guid Id { get; }
        public string FullName { get; }
        public string Address { get; }

        public CreateCustomer(Guid id, string fullName, string address)
        {
            Id = id;
            FullName = fullName;
            Address = address;
        }
    }
}