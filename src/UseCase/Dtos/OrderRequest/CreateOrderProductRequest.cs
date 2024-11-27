using Domain.Entities.Enums;

namespace UseCase.Dtos.OrderRequest;

public class CreateOrderProductRequest
{

    public int Quantity { get; init; }
    public int OrderId { get; init; }
    public ProductType productType { get; init; }
    public string Name { get; init; } = string.Empty; 
}
