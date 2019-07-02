using System;

namespace Pacco.Services.Customers.Core.Exceptions
{
    public class InvalidCustomerAddressException : ExceptionBase
    {
        public override string Code => "invalid_customer_address";
        public Guid Id { get; }
        public string Address { get; }

        public InvalidCustomerAddressException(Guid id, string address) : base(
            $"Customer with id: {id} has invalid address.")
        {
            Id = id;
            Address = address;
        }
    }
}