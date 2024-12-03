using Domain.Repositories;
using Infrastructure.Adapters;
using Infrastructure.Context;
using Infrastructure.Context.Interfaces;
using Infrastructure.Repositories;
using Infrastructure.Repositories.Interfaces;
using MongoDB.Driver.Core.Configuration;

namespace Api.Extensions;

public static class InfrastructureExtension
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        return
            services
                .AddContext()
                .AddRepository()
                .AddAdapters();
    }

    private static IServiceCollection AddContext(this IServiceCollection services)
    {

        var connectionString = Environment.GetEnvironmentVariable("MongoConnectionString");
        var dbName = Environment.GetEnvironmentVariable("dinersPayment");
        return
            services.AddSingleton<IMongoContext>(new MongoContext(connectionString, dbName));
    }

    private static IServiceCollection AddRepository(this IServiceCollection services)
    {
        return
            services.AddSingleton<IOrderMongoDbRepository, OrderMongoDbRepository>();
    }

    private static IServiceCollection AddAdapters(this IServiceCollection services)
    {
        return
            services.AddSingleton<IOrderRepository, OrderRepositoryAdapter>();
    }
}
