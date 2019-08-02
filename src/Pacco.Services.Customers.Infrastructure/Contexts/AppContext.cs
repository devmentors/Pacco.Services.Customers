using System.Collections.Generic;
using Pacco.Services.Customers.Application;

namespace Pacco.Services.Customers.Infrastructure.Contexts
{
    internal class AppContext : IAppContext
    {
        public string RequestId { get; }
        public IIdentityContext Identity { get; }
        public IDictionary<string, string> Claims { get; }

        internal AppContext()
        {
            Identity = new IdentityContext();
        }

        internal AppContext(CorrelationContext context)
        {
            RequestId = context.CorrelationId;
            Identity = new IdentityContext(context.User);
        }

        internal AppContext(string requestId, IIdentityContext identity)
        {
            RequestId = requestId;
            Identity = identity;
        }
    }
}