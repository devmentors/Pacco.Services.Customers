namespace Pacco.Services.Customers.Core.Exceptions
{
    public class InvalidAggregateIdException : DomainException
    {
        public override string Code => "invalid_aggregate_id";
        
        public InvalidAggregateIdException() : base($"Invalid aggregate id.")
        {
        }
    }
}