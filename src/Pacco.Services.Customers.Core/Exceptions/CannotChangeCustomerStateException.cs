using System;
using Pacco.Services.Customers.Core.Entities;

namespace Pacco.Services.Customers.Core.Exceptions
{
    public class CannotChangeCustomerStateException : ExceptionBase
    {
        public override string Code => "cannot_change_customer_state";
        public Guid Id { get; }
        public State State { get; }

        public CannotChangeCustomerStateException(Guid id, State state) : base(
            $"Cannot change customer: {id} state to: {state}.")
        {
            Id = id;
            State = state;
        }
    }
}