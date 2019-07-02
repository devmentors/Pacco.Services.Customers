using System;
using System.Collections.Generic;
using System.Linq;
using Pacco.Services.Customers.Core.Exceptions;

namespace Pacco.Services.Customers.Core.Entities
{
    public class Customer : AggregateRoot
    {
        private ISet<Guid> _completedOrders = new HashSet<Guid>();

        public string Email { get; private set; }
        public string FullName { get; private set; }
        public string Address { get; private set; }
        public bool IsVip { get; private set; }
        public bool RegistrationCompleted { get; private set; }
        public DateTime CreatedAt { get; private set; }

        public IEnumerable<Guid> CompletedOrders
        {
            get => _completedOrders;
            set => _completedOrders = new HashSet<Guid>(value);
        }

        public Customer(Guid id, string email, DateTime createdAt) : this(id, email, createdAt, string.Empty,
            string.Empty, false, Enumerable.Empty<Guid>(),false)
        {
        }

        public Customer(Guid id, string email, DateTime createdAt, string fullName, string address, bool isVip,
            IEnumerable<Guid> completedOrders = null, bool registrationCompleted = true)
        {
            Id = id;
            Email = email;
            CreatedAt = createdAt;
            FullName = fullName;
            Address = address;
            IsVip = isVip;
            CompletedOrders = completedOrders ?? Enumerable.Empty<Guid>();
            RegistrationCompleted = registrationCompleted;
        }

        public void CompleteRegistration(string fullName, string address)
        {
            if (string.IsNullOrWhiteSpace(fullName))
            {
                throw new InvalidCustomerFullNameException(Id, fullName);
            }

            if (string.IsNullOrWhiteSpace(address))
            {
                throw new InvalidCustomerAddressException(Id, address);
            }
            
            FullName = fullName;
            Address = address;
            RegistrationCompleted = true;
        }

        public void SetVip()
        {
            IsVip = true;
        }

        public void AddCompletedOrder(Guid orderId)
        {
            if (orderId == Guid.Empty)
            {
                return;
            }

            _completedOrders.Add(orderId);
        }
    }
}