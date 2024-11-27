using Domain.Entities.OrderAggregate;

namespace UseCase.Dtos.OrderRequest;

public class CreateOrderRequest
{
    public int Id { get; init; }
    public IEnumerable<CreateOrderProductRequest> OrderProducts { get; init; } = [];
}
