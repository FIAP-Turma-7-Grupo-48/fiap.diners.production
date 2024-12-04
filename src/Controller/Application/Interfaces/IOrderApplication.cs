using Controller.Dtos.OrderResponse;
using Domain.Entities.Enums;
using UseCase.Dtos.OrderRequest;

namespace Controller.Application.Interfaces;

public interface IOrderApplication
{
    Task<IEnumerable<GetOrListOrderResponse>> ListAsync(OrderStatus orderStatus, int? page, int? limit, CancellationToken cancellationToken);
    Task<IEnumerable<GetOrListOrderResponse>> ListActiveAsync(int? page, int? limit, CancellationToken cancellationToken);
    Task<CreateOrderResponse?> CreateAsync(CreateOrderRequest orderCreateRequest, CancellationToken cancellationToken);
    Task UpdateStatusToPreparing(string orderId, CancellationToken cancellationToken);
    Task UpdateStatusToDone(string orderId, CancellationToken cancellationToken);
    Task UpdateStatusToFinished(string orderId, CancellationToken cancellationToken);
}
