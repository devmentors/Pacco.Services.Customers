using System.Threading.Tasks;
using Convey.CQRS.Events;
using Microsoft.Extensions.Logging;
using Pacco.Services.Customers.Application.Services;
using Pacco.Services.Customers.Core.Entities;
using Pacco.Services.Customers.Core.Repositories;

namespace Pacco.Services.Customers.Application.Events.External.Handlers
{
    public class SignedUpHandler : IEventHandler<SignedUp>
    {
        private const string RequiredRole = "user";
        private readonly ICustomerRepository _customerRepository;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly ILogger<SignedUpHandler> _logger;

        public SignedUpHandler(ICustomerRepository customerRepository, IDateTimeProvider dateTimeProvider,
            ILogger<SignedUpHandler> logger)
        {
            _customerRepository = customerRepository;
            _dateTimeProvider = dateTimeProvider;
            _logger = logger;
        }

        public async Task HandleAsync(SignedUp @event)
        {
            if (@event.Role != RequiredRole)
            {
                _logger.LogWarning($"Customer account will not be created for the user with id: {@event.UserId} " +
                                   $"due to the invalid role: {@event.Role} (required: {RequiredRole}).");
                return;
            }

            var customer = await _customerRepository.GetAsync(@event.UserId);
            if (!(customer is null))
            {
                _logger.LogWarning($"Customer with id: {@event.UserId} was already created.");
                return;
            }

            customer = new Customer(@event.UserId, @event.Email, _dateTimeProvider.Now);
            await _customerRepository.AddAsync(customer);
            _logger.LogInformation($"Created a new customer with id: {@event.UserId}.");
        }
    }
}