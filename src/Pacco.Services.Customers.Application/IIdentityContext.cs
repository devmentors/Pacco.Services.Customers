namespace Pacco.Services.Customers.Application
{
    public interface IIdentityContext
    {
        string Id { get; }
        string Role { get; }
        bool IsAuthenticated { get; }
    }
}