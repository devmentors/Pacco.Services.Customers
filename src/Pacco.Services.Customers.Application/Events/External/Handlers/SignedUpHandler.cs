using System.Threading.Tasks;
using Convey.CQRS.Events;
using Pacco.Services.Customers.Application.Services;
using Pacco.Services.Customers.Core.Entities;
using Pacco.Services.Customers.Core.Repositories;

namespace Pacco.Services.Customers.Application.Events.External.Handlers
{
    public class SignedUpHandler : IEventHandler<SignedUp>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IDateTimeProvider _dateTimeProvider;

        public SignedUpHandler(ICustomerRepository customerRepository, IDateTimeProvider dateTimeProvider)
        {
            _customerRepository = customerRepository;
            _dateTimeProvider = dateTimeProvider;
        }

        public async Task HandleAsync(SignedUp @event)
        {
            if (@event.Role != "user")
            {
                return;
            }
            
            var customer = await _customerRepository.GetAsync(@event.UserId);
            if (!(customer is null))
            {
                return;
            }

            customer = new Customer(@event.UserId, @event.Email, _dateTimeProvider.Now);
            await _customerRepository.AddAsync(customer);
        }
    }
}