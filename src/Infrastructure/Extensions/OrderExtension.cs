using Domain.Entities.OrderAggregate;
using Infrastructure.MongoModels;

namespace Infrastructure.Extensions;

internal static class OrderExtension
{
    public static OrderMongoModel ToOrderMongoModel(this Order order)
    {

        var orderProducts = order.OrderProducts.Select(x => x.ToOrderProductMongoModel()).ToList();
        OrderMongoModel response = new()
        {
            Id = order.Id,
            ExternalOrderId = order.ExternalOrderId,
            Status = order.Status,
            OrderProducts = orderProducts
        };

        return
            response;
    }
}
