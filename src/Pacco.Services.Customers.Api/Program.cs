using System.Collections.Generic;
using System.Threading.Tasks;
using Convey;
using Convey.Logging;
using Convey.WebApi;
using Convey.WebApi.CQRS;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Pacco.Services.Customers.Application;
using Pacco.Services.Customers.Application.Commands;
using Pacco.Services.Customers.Application.DTO;
using Pacco.Services.Customers.Application.Queries;
using Pacco.Services.Customers.Infrastructure;

namespace Pacco.Services.Customers.Api
{
    public class Program
    {
        public static async Task Main(string[] args)
            => await WebHost.CreateDefaultBuilder(args)
                .ConfigureServices(services => services
                    .AddOpenTracing()
                    .AddConvey()
                    .AddWebApi()
                    .AddApplication()
                    .AddInfrastructure()
                    .Build())
                .Configure(app => app
                    .UseInfrastructure()
                    .UseDispatcherEndpoints(endpoints => endpoints
                        .Get("", ctx => ctx.Response.WriteAsync("Welcome to Pacco Customers Service!"))
                        .Get<GetCustomer, CustomerDto>("customers/{id}")
                        .Get<GetCustomers, IEnumerable<CustomerDto>>("customers")
                        .Post<CompleteCustomerRegistration>("customers",
                            afterDispatch: (cmd, ctx) => ctx.Response.Created($"customers/{cmd.Id}"))))
                .UseLogging()
                .Build()
                .RunAsync();
    }
}