using System;

namespace Pacco.Services.Customers.Core.Exceptions
{
    public class CustomerNotFoundException : ExceptionBase
    {
        public override string Code => "customer_not_found";

        public CustomerNotFoundException(Guid customerId) : base($"Customer with id: {customerId} was not found.")
        {
        }
    }
}