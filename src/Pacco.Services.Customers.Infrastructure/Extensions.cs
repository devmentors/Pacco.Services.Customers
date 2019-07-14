using System;
using Convey;
using Convey.CQRS.Queries;
using Convey.Discovery.Consul;
using Convey.HTTP;
using Convey.LoadBalancing.Fabio;
using Convey.MessageBrokers.CQRS;
using Convey.MessageBrokers.RabbitMQ;
using Convey.Metrics.AppMetrics;
using Convey.Persistence.MongoDB;
using Convey.Tracing.Jaeger;
using Convey.Tracing.Jaeger.RabbitMQ;
using Convey.WebApi;
using Convey.WebApi.CQRS;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Pacco.Services.Customers.Application;
using Pacco.Services.Customers.Application.Commands;
using Pacco.Services.Customers.Application.Events.External;
using Pacco.Services.Customers.Application.Services;
using Pacco.Services.Customers.Core.Repositories;
using Pacco.Services.Customers.Core.Services;
using Pacco.Services.Customers.Infrastructure.Exceptions;
using Pacco.Services.Customers.Infrastructure.Mongo.Documents;
using Pacco.Services.Customers.Infrastructure.Mongo.Repositories;
using Pacco.Services.Customers.Infrastructure.Services;

namespace Pacco.Services.Customers.Infrastructure
{
    public static class Extensions
    {
        public static IConveyBuilder AddInfrastructure(this IConveyBuilder builder)
        {
            builder.Services.AddSingleton<IEventMapper, EventMapper>();
            builder.Services.AddTransient<IMessageBroker, MessageBroker>();
            builder.Services.AddTransient<ICustomerRepository, CustomerMongoRepository>();
            builder.Services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
            builder.Services.AddSingleton<IVipPolicy, VipPolicy>();

            return builder
                .AddQueryHandlers()
                .AddInMemoryQueryDispatcher()
                .AddHttpClient()
                .AddConsul()
                .AddFabio()
                .AddRabbitMq(plugins: p => p.RegisterJaeger())
                .AddExceptionToMessageMapper<ExceptionToMessageMapper>()
                .AddMongo()
                .AddMetrics()
                .AddJaeger()
                .AddMongoRepository<CustomerDocument, Guid>("Customers");
        }

        public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder app)
        {
            app.UseErrorHandler()
                .UseJaeger()
                .UseInitializers()
                .UsePublicContracts<ContractAttribute>()
                .UseConsul()
                .UseMetrics()
                .UseRabbitMq()
                .SubscribeCommand<CompleteCustomerRegistration>()
                .SubscribeEvent<SignedUp>()
                .SubscribeEvent<OrderCompleted>();

            return app;
        }
    }
}