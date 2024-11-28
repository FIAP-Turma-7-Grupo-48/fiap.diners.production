using Domain.Entities.Enums;
using Helpers;
using Infrastructure.Context.Interfaces;
using Infrastructure.MongoModels;
using Infrastructure.Repositories.Interfaces;
using MongoDB.Driver;

namespace Infrastructure.Repositories;

public class OrderMongoDbRepository : IOrderMongoDbRepository
{
    private readonly IMongoCollection<OrderMongoModel> _collection;

    public OrderMongoDbRepository(IMongoContext mongoContext)
    {
        _collection = mongoContext.GetCollection<OrderMongoModel>();
    }

    public async Task<OrderMongoModel> CreateAsync(OrderMongoModel orderMongoModel, CancellationToken cancellationToken)
    {

        await _collection.InsertOneAsync(orderMongoModel, default, cancellationToken);

        return orderMongoModel;
    }

    public async Task<OrderMongoModel?> GetAsync(string id, CancellationToken cancellationToken)
    {

        var filters = Builders<OrderMongoModel>
            .Filter
            .Eq(x => x.Id, id);

        var response = await _collection
            .Find(filters)
            .FirstOrDefaultAsync(cancellationToken);

        return
            response;
    }

    public async Task<IEnumerable<OrderMongoModel>> ListAsync(IEnumerable<OrderStatus> orderStatus, int? page, int? limit, CancellationToken cancellationToken)
    {
        var take = limit ?? int.MaxValue;

        var skip = MathHelper.CalculatePaginateSkip(page, take);

        var findOptions = new FindOptions<OrderMongoModel>()
        {
            Skip = skip,
            Limit = take
        };

        var filters = Builders<OrderMongoModel>
            .Filter
            .In(x => x.Status, orderStatus);

        var orders = await _collection
            .FindAsync(filters, findOptions, cancellationToken);

        return orders.ToEnumerable();
    }

    public async Task<OrderMongoModel> ReplaceOneAsync(OrderMongoModel orderMongoModel, CancellationToken cancellationToken)
    {
        var filters = Builders<OrderMongoModel>
            .Filter
            .Eq(x => x.Id, orderMongoModel.Id);

        var options = new FindOneAndReplaceOptions<OrderMongoModel>()
        {
            ReturnDocument = ReturnDocument.After,
            IsUpsert = false
        };

        var replacedOrder = await _collection.FindOneAndReplaceAsync(filters, orderMongoModel, options, cancellationToken);

        return replacedOrder;
    }
}
