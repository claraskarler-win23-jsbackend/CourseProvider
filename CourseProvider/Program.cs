using Infrastructure.GraphQl;
using Infrastructure.GraphQl.ObjectTypes;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = new HostBuilder()
    .ConfigureFunctionsWebApplication()
    .ConfigureServices(services =>
    {
        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();

        services.AddGraphQLFunction()
                .AddQueryType<Query>()
                .AddType<CourseType>();

    })
    .Build();

host.Run();
