using System.Collections.Generic;

namespace Pacco.Services.Customers.Application
{
    public interface IAppContext
    {
        string RequestId { get; }
        IIdentityContext Identity { get; }
        IDictionary<string, string> Claims { get; }
    }
}