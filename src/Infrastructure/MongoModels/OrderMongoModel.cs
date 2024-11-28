using Domain.Entities.Enums;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Diagnostics.CodeAnalysis;

namespace Infrastructure.MongoModels;

[ExcludeFromCodeCoverage]
[BsonIgnoreExtraElements]
[BsonDiscriminator("order")]
public class OrderMongoModel
{
    public OrderMongoModel()
    {
        Created = DateTime.UtcNow;
    }
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    [BsonRepresentation(BsonType.String)]
    public int ExternalOrderId { get; set; }
    public OrderStatus Status { get; set; }
    public DateTime Created { get; set; }
    public required IEnumerable<OrderProductMongoModel> OrderProducts { get; set; }
}
