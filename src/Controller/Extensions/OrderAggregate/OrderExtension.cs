using Controller.Dtos.OrderResponse;
using Domain.Entities.OrderAggregate;

namespace Controller.Extensions.OrderAggregate;

internal static class OrderExtension
{

	public static GetOrListOrderResponse ToGetOrderResponse(this Order order)
	{
		List<GetOrderProductReponse> orderProductsResponse = [];

		foreach (var orderProduct in order.OrderProducts)
		{
			var getOrderProduct = orderProduct.ToGetOrderProductReponse();
			orderProductsResponse.Add(getOrderProduct);
		}

		GetOrListOrderResponse response = new()
		{
			Id = order.Id,
			OrderProducts = orderProductsResponse,
			Status = order.Status,
		};

		return response;
	}

	public static CreateOrderResponse ToCreateOrderResponse(this Order order)
	{
		return new() { 
			OrderId = order.Id 
		};
	}
}
