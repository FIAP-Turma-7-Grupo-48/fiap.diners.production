using Domain.Entities.Enums;
using Domain.Entities.OrderAggregate;

namespace Infrastructure.MongoModels.Extensions;

internal static class OrderProductMongoModelExtension
{
    public static OrderProduct ToOrderProduct(this OrderProductMongoModel orderProductMongoModel)
    {
        OrderProduct orderProduct = new()
        {
            Quantity = orderProductMongoModel.Quantity,
            Name = orderProductMongoModel.Name,
            ProductType = orderProductMongoModel.ProductType,
        };

        return 
            orderProduct;
    }
}
