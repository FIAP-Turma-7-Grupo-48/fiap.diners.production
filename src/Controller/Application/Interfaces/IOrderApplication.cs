using UseCase.Dtos.OrderRequest;
using Controller.Dtos.OrderResponse;
using Domain.Entities.Enums;
using Domain.Entities.OrderAggregate;

namespace Controller.Application.Interfaces;

public interface IOrderApplication
{
	Task<IEnumerable<GetOrListOrderResponse>> ListAsync(OrderStatus orderStatus, int? page, int? limit, CancellationToken cancellationToken);
	Task<IEnumerable<GetOrListOrderResponse>> ListActiveAsync(int? page, int? limit, CancellationToken cancellationToken);
	Task<CreateOrderResponse?> CreateAsync(CreateOrderRequest orderCreateRequest, CancellationToken cancellationToken);
	Task UpdateStatusToPreparing(int orderId, CancellationToken cancellationToken);
	Task UpdateStatusToDone(int orderId, CancellationToken cancellationToken);
	Task UpdateStatusToFinished(int orderId, CancellationToken cancellationToken);
}
