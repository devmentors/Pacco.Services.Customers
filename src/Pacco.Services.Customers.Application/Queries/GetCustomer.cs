using System;
using Convey.CQRS.Queries;
using Pacco.Services.Customers.Application.DTO;

namespace Pacco.Services.Customers.Application.Queries
{
    public class GetCustomer : IQuery<CustomerDto>
    {
        public Guid Id { get; set; }
    }
}