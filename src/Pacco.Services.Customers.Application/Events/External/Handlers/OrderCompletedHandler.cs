using System.Threading.Tasks;
using Convey.CQRS.Events;
using Pacco.Services.Customers.Application.Services;
using Pacco.Services.Customers.Core.Exceptions;
using Pacco.Services.Customers.Core.Repositories;
using Pacco.Services.Customers.Core.Services;

namespace Pacco.Services.Customers.Application.Events.External.Handlers
{
    public class OrderCompletedHandler : IEventHandler<OrderCompleted>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly IVipPolicy _vipPolicy;

        public OrderCompletedHandler(ICustomerRepository customerRepository, IDateTimeProvider dateTimeProvider,
            IVipPolicy vipPolicy)
        {
            _customerRepository = customerRepository;
            _dateTimeProvider = dateTimeProvider;
            _vipPolicy = vipPolicy;
        }

        public async Task HandleAsync(OrderCompleted @event)
        {
            var customer = await _customerRepository.GetAsync(@event.CustomerId);
            if (customer is null)
            {
                throw new CustomerNotFoundException(@event.CustomerId);
            }

            customer.AddCompletedOrder(@event.OrderId);
            _vipPolicy.ApplyVipStatusIfEligible(customer);
            await _customerRepository.UpdateAsync(customer);
        }
    }
}