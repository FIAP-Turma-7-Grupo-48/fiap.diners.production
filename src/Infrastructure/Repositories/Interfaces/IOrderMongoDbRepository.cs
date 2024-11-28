using Domain.Entities.Enums;
using Infrastructure.MongoModels;

namespace Infrastructure.Repositories.Interfaces;

public interface IOrderMongoDbRepository
{
    Task<OrderMongoModel> CreateAsync(OrderMongoModel orderMongoModel, CancellationToken cancellationToken);
    Task<OrderMongoModel?> GetAsync(string id, CancellationToken cancellationToken);
    Task<IEnumerable<OrderMongoModel>> ListAsync(IEnumerable<OrderStatus> orderStatus, int? page, int? limit, CancellationToken cancellationToken);
    Task<OrderMongoModel> ReplaceOneAsync(OrderMongoModel orderMongoModel, CancellationToken cancellationToken);

}
