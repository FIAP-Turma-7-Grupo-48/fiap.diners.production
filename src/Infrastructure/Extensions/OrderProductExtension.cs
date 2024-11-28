using Domain.Entities.OrderAggregate;
using Infrastructure.MongoModels;

namespace Infrastructure.Extensions;

public static class OrderProductExtension
{
    public static OrderProductMongoModel ToOrderProductMongoModel(this OrderProduct orderProduct)
    {
        OrderProductMongoModel response = new()
        {
            Quantity = orderProduct.Quantity,   
            Name = orderProduct.Name,
            ProductType = orderProduct.ProductType 
        };
        return 
            response;
    }
}
