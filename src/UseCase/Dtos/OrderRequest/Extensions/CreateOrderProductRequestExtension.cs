using Domain.Entities.OrderAggregate;

namespace UseCase.Dtos.OrderRequest.Extensions;

internal static class CreateOrderProductRequestExtension
{
    public static OrderProduct ToOrderProduct(this CreateOrderProductRequest request)
    {
        var response = new OrderProduct() { 
            OrderId = request.OrderId,
            Name = request.Name,
            Quantity = request.Quantity,
            ProductType = request.productType
        };

        return response;
    }

    public static IEnumerable<OrderProduct> ToOrderProduct(this IEnumerable<CreateOrderProductRequest> request)
    {
        var response = new List<OrderProduct>();
        foreach(var item in request)
        {
            var orderProduct = item.ToOrderProduct();
            response.Add(orderProduct);
        }

        return response;
         
    }
}
