using UseCase.Services;
using UseCase.Services.Interfaces;
using Core.Notifications;

namespace Api.Extensions;

public static class UseCaseExtension
{
	public static IServiceCollection AddUseCase(this IServiceCollection services)
	{
		return
			services
				.AddServices()
				.AddNotifications();
	}

	private static IServiceCollection AddServices(this IServiceCollection services)
	{
		return services
			.AddScoped<IOrderUseCase, OrderUseCase>();
	}

	private static IServiceCollection AddNotifications(this IServiceCollection services)
	{
		return services
			.AddScoped<NotificationContext>();
	}
}