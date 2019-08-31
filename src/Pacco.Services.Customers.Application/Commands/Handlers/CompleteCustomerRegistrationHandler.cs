using System.Linq;
using System.Threading.Tasks;
using Convey.CQRS.Commands;
using Microsoft.Extensions.Logging;
using Pacco.Services.Customers.Application.Services;
using Pacco.Services.Customers.Core.Entities;
using Pacco.Services.Customers.Core.Exceptions;
using Pacco.Services.Customers.Core.Repositories;

namespace Pacco.Services.Customers.Application.Commands.Handlers
{
    public class CompleteCustomerRegistrationHandler : ICommandHandler<CompleteCustomerRegistration>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IEventMapper _eventMapper;
        private readonly IMessageBroker _messageBroker;
        private readonly ILogger<CompleteCustomerRegistrationHandler> _logger;

        public CompleteCustomerRegistrationHandler(ICustomerRepository customerRepository, IEventMapper eventMapper,
            IMessageBroker messageBroker, ILogger<CompleteCustomerRegistrationHandler> logger)
        {
            _customerRepository = customerRepository;
            _eventMapper = eventMapper;
            _messageBroker = messageBroker;
            _logger = logger;
        }

        public async Task HandleAsync(CompleteCustomerRegistration command)
        {
            var customer = await _customerRepository.GetAsync(command.CustomerId);
            if (customer is null)
            {
                throw new CustomerNotFoundException(command.CustomerId);
            }

            if (customer.State == State.Valid)
            {
                throw new CannotChangeCustomerStateException(command.CustomerId, State.Valid);
            }

            customer.CompleteRegistration(command.FullName, command.Address);
            await _customerRepository.UpdateAsync(customer);
            var events = _eventMapper.MapAll(customer.Events);
            await _messageBroker.PublishAsync(events.ToArray());
            _logger.LogInformation($"Completed a registration for the customer with id: {command.CustomerId}.");
        }
    }
}