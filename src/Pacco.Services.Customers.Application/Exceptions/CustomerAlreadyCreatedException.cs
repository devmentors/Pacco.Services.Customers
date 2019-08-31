using System;
using Pacco.Services.Customers.Core.Exceptions;

namespace Pacco.Services.Customers.Application.Exceptions
{
    public class CustomerAlreadyCreatedException: ExceptionBase
    {
        public override string Code => "customer_already_created";
        public Guid CustomerId { get; }

        public CustomerAlreadyCreatedException(Guid customerId)
            : base($"Customer with id: {customerId} was already created.")
        {
            CustomerId = customerId;
        }
    }
}