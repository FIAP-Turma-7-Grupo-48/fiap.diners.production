using Domain.Entities.OrderAggregate;
using Infrastructure.MongoModels;

namespace Infrastructure.MongoModels.Extensions;

public static class OrderMongoModelExtension
{
    public static Order ToOrder(this OrderMongoModel orderMongoModel)
    {
        var orderProduct = orderMongoModel.OrderProducts.Select(x => x.ToOrderProduct()).ToList();

        Order order = new(orderMongoModel.Id, orderMongoModel.Created, orderMongoModel.Status)
        {
            ExternalOrderId = orderMongoModel.ExternalOrderId,
            OrderProducts = orderProduct
        };

        return
            order;
    }
}
