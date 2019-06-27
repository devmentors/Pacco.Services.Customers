using Pacco.Services.Customers.Application.DTO;
using Pacco.Services.Customers.Core.Entities;

namespace Pacco.Services.Customers.Infrastructure.Mongo.Documents
{
    public static class Extensions
    {
        public static Customer AsEntity(this CustomerDocument document)
            => new Customer(document.Id, document.Email, document.CreatedAt, document.FullName, document.Address,
                document.IsVip, document.CompletedOrders, document.RegistrationCompleted);

        public static CustomerDocument AsDocument(this Customer entity)
            => new CustomerDocument
            {
                Id = entity.Id,
                Email = entity.Email,
                FullName = entity.FullName,
                Address = entity.Address,
                IsVip = entity.IsVip,
                RegistrationCompleted = entity.RegistrationCompleted,
                CreatedAt = entity.CreatedAt,
                CompletedOrders = entity.CompletedOrders
            };

        public static CustomerDto AsDto(this CustomerDocument document)
            => new CustomerDto
            {
                Id = document.Id,
                Email = document.Email,
                FullName = document.FullName,
                Address = document.Address,
                IsVip = document.IsVip,
                RegistrationCompleted = document.RegistrationCompleted,
                CreatedAt = document.CreatedAt,
                CompletedOrders = document.CompletedOrders
            };
    }
}