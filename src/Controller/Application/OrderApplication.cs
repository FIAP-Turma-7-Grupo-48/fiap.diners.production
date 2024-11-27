using Controller.Application.Interfaces;
using Controller.Dtos.OrderResponse;
using Controller.Extensions.OrderAggregate;
using Domain.Entities.Enums;
using UseCase.Dtos.OrderRequest;
using UseCase.Services.Interfaces;

namespace Controller.Application;

public class OrderApplication : IOrderApplication
{
	private readonly IOrderUseCase _orderUseCase;
	public OrderApplication(IOrderUseCase orderUseCase)
	{
		_orderUseCase = orderUseCase;
	}

	public async Task<IEnumerable<GetOrListOrderResponse>> ListAsync(OrderStatus orderStatus, int? page, int? limit, CancellationToken cancellationToken)
	{
		var order = await _orderUseCase.ListAsync(orderStatus, page, limit, cancellationToken);

		return
			order.Select(x => x.ToGetOrderResponse());
	}

	public async Task<IEnumerable<GetOrListOrderResponse>> ListActiveAsync(int? page, int? limit, CancellationToken cancellationToken)
	{
		var order = await _orderUseCase.ListActiveAsync(page, limit, cancellationToken);

		return
			order.Select(x => x.ToGetOrderResponse());
	}

	public async Task<CreateOrderResponse?> CreateAsync(CreateOrderRequest orderCreateRequest, CancellationToken cancellationToken)
	{
		var order = await _orderUseCase.CreateAsync(orderCreateRequest, cancellationToken);
		if (order == null)
		{
			return null;
		}

		return new()
		{
			OrderId = order.Id
		};
	}

	public Task UpdateStatusToPreparing(int orderId, CancellationToken cancellationToken)
	{
		return _orderUseCase.UpdateStatusToPreparing(orderId, cancellationToken);
	}

	public Task UpdateStatusToDone(int orderId, CancellationToken cancellationToken)
	{
		return _orderUseCase.UpdateStatusToDone(orderId, cancellationToken);
	}

	public Task UpdateStatusToFinished(int orderId, CancellationToken cancellationToken)
	{
		return _orderUseCase.UpdateStatusToFinished(orderId, cancellationToken);
	}
}
