using Pacco.Services.Customers.Core.Exceptions;

namespace Pacco.Services.Customers.Application.Exceptions
{
    public class InvalidRoleException : ExceptionBase
    {
        public override string Code => "invalid_role";

        public InvalidRoleException(string role, string requiredRole)
            : base("Customer account will not be created for the user with id: {@event.UserId} " +
                   $"due to the invalid role: {role} (required: {requiredRole}).")
        {
        }
    }
}