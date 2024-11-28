using Domain.Entities.Enums;
using MongoDB.Bson.Serialization.Attributes;
using System.Diagnostics.CodeAnalysis;

namespace Infrastructure.MongoModels;

[ExcludeFromCodeCoverage]
[BsonIgnoreExtraElements]
public class OrderProductMongoModel
{
    public int Quantity { get; set; }
    public string Name { get; set; } = string.Empty;
    public ProductType ProductType { get; set; }
}
