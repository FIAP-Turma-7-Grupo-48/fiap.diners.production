using Controller.Dtos.OrderResponse;
using Domain.Entities.OrderAggregate;

namespace Controller.Extensions.OrderAggregate;

internal static class OrderProductExtension
{

	public static GetOrderProductReponse ToGetOrderProductReponse(this OrderProduct product)
	{
		GetOrderProductReponse getOrderProductReponse = new()
		{

			ProductName = product.Name,
			Quantity = product.Quantity,
		};

		return getOrderProductReponse;
	}
}
