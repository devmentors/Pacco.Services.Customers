using System;
using Pacco.Services.Customers.Core.Exceptions;

namespace Pacco.Services.Customers.Application.Exceptions
{
    public class CustomerAlreadyRegisteredException : ExceptionBase
    {
        public override string Code => "customer_already_registered";

        
        public CustomerAlreadyRegisteredException(Guid id) 
            : base($"Customer with id: {id} has already been registered.")
        {
        }
    }
}