using System;
using Convey.MessageBrokers.RabbitMQ;
using Pacco.Services.Customers.Application.Events.Rejected;
using Pacco.Services.Customers.Core.Exceptions;

namespace Pacco.Services.Customers.Infrastructure
{
    public class ExceptionToMessageMapper : IExceptionToMessageMapper
    {
        public object Map(Exception exception)
        {
            switch (exception)
            {
                case CustomerNotFoundException ex: return new CreateCustomerRejected(ex.Id, ex.Code, ex.Message);
            }

            return null;
        }
    }
}