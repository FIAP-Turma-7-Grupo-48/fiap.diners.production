using Domain.Entities.Enums;
using Domain.Entities.OrderAggregate;
using Domain.Repositories;
using Infrastructure.Extensions;
using Infrastructure.MongoModels.Extensions;
using Infrastructure.Repositories.Interfaces;

namespace Infrastructure.Adapters;

public class OrderRepositoryAdapter : IOrderRepository
{
    private readonly IOrderMongoDbRepository _orderRepository;
    public OrderRepositoryAdapter(IOrderMongoDbRepository orderMongoDbRepository)
    {
        _orderRepository = orderMongoDbRepository;
    }
    public async Task<Order> CreateAsync(Order order, CancellationToken cancellationToken)
    {
        var orderMongoDb = order.ToOrderMongoModel();
        var orderDb = await _orderRepository.CreateAsync(orderMongoDb, cancellationToken);
        return
            orderDb.ToOrder();
    }

    public async Task<Order?> GetAsync(string id, CancellationToken cancellationToken)
    {
        var orderDb = await _orderRepository.GetAsync(id, cancellationToken);
        return
            orderDb?.ToOrder();
    }

    public async Task<IEnumerable<Order>> ListAsync(OrderStatus orderStatus, int? page, int? limit, CancellationToken cancellationToken)
    {
        var ordersStatusFilter = new List<OrderStatus>() { orderStatus };
        var orderDb = await _orderRepository.ListAsync(ordersStatusFilter, page, limit, cancellationToken);
        return
            orderDb.Select(x => x.ToOrder()).ToList();
    }

    public async Task<IEnumerable<Order>> ListAsync(IEnumerable<OrderStatus> orderStatus, int? page, int? limit, CancellationToken cancellationToken)
    {
        var orderDb = await _orderRepository.ListAsync(orderStatus, page, limit, cancellationToken);
        return
            orderDb.Select(x => x.ToOrder()).ToList();
    }

    public Task UpdateAsync(Order order, CancellationToken cancellationToken)
    {
        var orderMongoDb = order.ToOrderMongoModel();
        return _orderRepository.ReplaceOneAsync(orderMongoDb, cancellationToken);
    }
}
