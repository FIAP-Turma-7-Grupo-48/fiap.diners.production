using Domain.Repositories;
using Infrastructure.Adapters;
using Infrastructure.Context;
using Infrastructure.Context.Interfaces;
using Infrastructure.Repositories;
using Infrastructure.Repositories.Interfaces;

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
        return
            services.AddSingleton<IMongoContext>(new MongoContext("mongodb://sa:1234@localhost:27017/", "dinersProduction"));
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
