using Domain.Entities.Enums;
using Domain.Entities.Exceptions;

namespace Domain.Entities.OrderAggregate;

public class OrderProduct
{
    public int Quantity { get; init; }
    
    private readonly string _name = string.Empty;
    public required string Name
    {
        get => _name;
        init
        {
            EntityArgumentNullException.ThrowIfNullOrWhiteSpace(value, nameof(Name), GetType());

            _name = value;
        }

    }
    private readonly ProductType _productType;
    public required ProductType ProductType
    {
        get => _productType;
        init
        {
            if (value == ProductType.None)
            {
                throw new EntityArgumentEnumInvalidException(nameof(ProductType),
                    ProductType.None.ToString(), GetType().ToString());
            }

            _productType = value;
        }
    }

}