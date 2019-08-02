using System.Collections.Generic;
using Pacco.Services.Customers.Application;

namespace Pacco.Services.Customers.Infrastructure.Contexts
{
    internal class IdentityContext : IIdentityContext
    {
        public string Id { get; }
        public string Role { get; }
        public bool IsAuthenticated { get; }
        public IDictionary<string, string> Claims { get; }

        internal IdentityContext()
        {
        }

        internal IdentityContext(CorrelationContext.UserContext context)
            : this(context.Id, context.Role, context.IsAuthenticated, context.Claims)
        {
        }

        internal IdentityContext(string id, string role, bool isAuthenticated, IDictionary<string, string> claims)
        {
            Id = id;
            Role = role;
            IsAuthenticated = isAuthenticated;
            Claims = claims;
        }
    }
}