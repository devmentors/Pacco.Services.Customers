using System;
using Convey.MessageBrokers.RabbitMQ;
using Pacco.Services.Customers.Application.Commands;
using Pacco.Services.Customers.Application.Events.Rejected;
using Pacco.Services.Customers.Core.Exceptions;

namespace Pacco.Services.Customers.Infrastructure.Exceptions
{
    public class ExceptionToMessageMapper : IExceptionToMessageMapper
    {
        public object Map(Exception exception, object message)
        {
            switch (exception)
            {
                case CannotChangeCustomerStateException ex:
                {
                    switch (message)
                    {
                        case ChangeCustomerState _:
                            return new ChangeCustomerStateRejected(ex.Id, ex.State.ToString().ToLowerInvariant(),
                                ex.Message, ex.Code);
                        case CompleteCustomerRegistration _:
                            return new CompleteCustomerRegistrationRejected(ex.Id, ex.Message, ex.Code);
                    }
                }
                    break;

                case CustomerNotFoundException ex:
                    return new CompleteCustomerRegistrationRejected(ex.Id, ex.Message, ex.Code);
                case InvalidCustomerFullNameException ex:
                    return new CompleteCustomerRegistrationRejected(ex.Id, ex.Message, ex.Code);
                case InvalidCustomerAddressException ex:
                    return new CompleteCustomerRegistrationRejected(ex.Id, ex.Message, ex.Code);
            }

            return null;
        }
    }
}