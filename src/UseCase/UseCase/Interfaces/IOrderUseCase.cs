using Domain.Entities.Enums;
using Domain.Entities.OrderAggregate;
using UseCase.Dtos.OrderRequest;

namespace UseCase.Interfaces;

public interface IOrderUseCase
{
    Task<Order?> CreateAsync(CreateOrderRequest orderCreateRequest, CancellationToken cancellationToken);
    Task UpdateStatusToPreparing(string orderId, CancellationToken cancellationToken);
    Task UpdateStatusToDone(string orderId, CancellationToken cancellationToken);
    Task UpdateStatusToFinished(string orderId, CancellationToken cancellationToken);
    Task<IEnumerable<Order>> ListAsync(OrderStatus orderStatus, int? page, int? limit, CancellationToken cancellationToken);
    Task<IEnumerable<Order>> ListActiveAsync(int? page, int? limit, CancellationToken cancellationToken);
}
