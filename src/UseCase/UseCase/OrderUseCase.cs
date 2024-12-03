using Core.Notifications;
using Domain.Entities.Enums;
using Domain.Entities.OrderAggregate;
using Domain.Repositories;
using UseCase.Dtos.OrderRequest;
using UseCase.Dtos.OrderRequest.Extensions;
using UseCase.Services.Interfaces;

namespace UseCase;

public class OrderUseCase : IOrderUseCase
{
    private readonly IOrderRepository _orderRepository;
    private readonly NotificationContext _notificationContext;

    public OrderUseCase(
        IOrderRepository orderRepository,
        NotificationContext notificationContext
    )
    {
        _orderRepository = orderRepository;
        _notificationContext = notificationContext;
    }

    public async Task<Order?> CreateAsync(CreateOrderRequest orderCreateRequest,
        CancellationToken cancellationToken)
    {

        var orderProduct = orderCreateRequest.OrderProducts.ToOrderProduct();


        Order order = new()
        {
            ExternalOrderId = orderCreateRequest.Id,
            OrderProducts = orderProduct,
        };

        order = await _orderRepository.CreateAsync(order, cancellationToken);

        return order;

    }

    public async Task UpdateStatusToPreparing(int orderId, CancellationToken cancellationToken)
    {

        var order = await _orderRepository.GetAsync(orderId, cancellationToken);

        _notificationContext.AssertArgumentNotNull(order, $"Order with id:{orderId} not found");

        if (_notificationContext.HasErrors)
        {
            return;
        }

        order!.ChangeStatusToPreparing();

        await _orderRepository.UpdateAsync(order, cancellationToken);
    }
    public async Task UpdateStatusToDone(int orderId, CancellationToken cancellationToken)
    {

        var order = await _orderRepository.GetAsync(orderId, cancellationToken);

        _notificationContext.AssertArgumentNotNull(order, $"Order with id:{orderId} not found");

        if (_notificationContext.HasErrors)
        {
            return;
        }

        order!.ChangeStatusToDone();

        await _orderRepository.UpdateAsync(order, cancellationToken);
    }

    public async Task UpdateStatusToFinished(int orderId, CancellationToken cancellationToken)
    {

        var order = await _orderRepository.GetAsync(orderId, cancellationToken);

        _notificationContext.AssertArgumentNotNull(order, $"Order with id:{orderId} not found");

        if (_notificationContext.HasErrors)
        {
            return;
        }

        order!.ChangeStatusToFinished();

        await _orderRepository.UpdateAsync(order, cancellationToken);
    }

    public Task<IEnumerable<Order>> ListAsync(OrderStatus orderStatus, int? page, int? limit, CancellationToken cancellationToken)
    {
        return
             _orderRepository.ListAsync(orderStatus, page, limit, cancellationToken);
    }

    public async Task<IEnumerable<Order>> ListActiveAsync(int? page, int? limit, CancellationToken cancellationToken)
    {
        var status = new List<OrderStatus>()
        {
            OrderStatus.Done,
            OrderStatus.Preparing,
            OrderStatus.Received
        };
        var order = await _orderRepository.ListAsync(status, page, limit, cancellationToken);

        order = order.OrderByDescending(x => x.Status).ThenBy(x => x.Created);
        return order;

    }
}
