using Controller.Application;
using Controller.Application.Interfaces;

namespace Api.Extensions;

public static class ControllerLayerExtension
{
    public static IServiceCollection AddControllerLayerDI(this IServiceCollection services)
    {
        return
            services
                .AddApplication();
    }

    private static IServiceCollection AddApplication(this IServiceCollection services)
    {
        return services
            .AddScoped<IOrderApplication, OrderApplication>();


    }
}
