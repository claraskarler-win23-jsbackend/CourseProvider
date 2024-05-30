using Infrastructure.Data.Contexts;
using Infrastructure.GraphQl;
using Infrastructure.GraphQl.Mutations;
using Infrastructure.GraphQl.ObjectTypes;
using Infrastructure.Services;
using Microsoft.Azure.Functions.Worker;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = new HostBuilder()
    .ConfigureFunctionsWebApplication()
    .ConfigureServices(services =>
    {
        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();
        services.AddPooledDbContextFactory<DataContext>(x =>
        {
            x.UseCosmos(Environment.GetEnvironmentVariable("COSMOS_URI")!, Environment.GetEnvironmentVariable("COSMOS_DBNAME")!)
            .UseLazyLoadingProxies();

        });

        services.AddScoped<ICourseService, CourseService>();
        
        services.AddGraphQLFunction()
                .AddQueryType<Query>()
                .AddMutationType<CourseMutation>()
                .AddType<CourseType>();

        var sp = services.BuildServiceProvider();
        using var scope = sp.CreateScope();
        var dbContextFactory = scope.ServiceProvider.GetRequiredService<IDbContextFactory<DataContext>>();
        using var context = dbContextFactory.CreateDbContext();
        context.Database.EnsureCreated();

    })
    .Build();

host.Run();
