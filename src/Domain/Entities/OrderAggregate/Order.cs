using Domain.Entities.Base.Interfaces;
using Domain.Entities.Enums;
using Domain.Entities.OrderAggregate.Exceptions;

namespace Domain.Entities.OrderAggregate;

public class Order : IAggregateRoot
{
    public Order()
    {
        
    }
    public Order(string id, DateTime created, OrderStatus status) 
    { 
        Id = id;
        Created = created;
        Status = status;
    }

    public string Id { get; private init; } 
    public int ExternalOrderId { get; init; }

    public OrderStatus Status { get; private set; } = OrderStatus.Received;

    public required IEnumerable<OrderProduct> OrderProducts { get; init; }
    public DateTime? Created { get; private init; }

    public Order ChangeStatusToPreparing()
    {
        ChangeOrderStatusInvalidException.ThrowIfOrderStatusInvalidStepChange(
            Status,
            OrderStatus.Received,
            OrderStatus.Preparing);

        Status = OrderStatus.Preparing;
        return this;
    }

    public Order ChangeStatusToDone()
    {
        ChangeOrderStatusInvalidException.ThrowIfOrderStatusInvalidStepChange(
            Status,
            OrderStatus.Preparing,
            OrderStatus.Done);

        Status = OrderStatus.Done;
        return this;
    }

    public Order ChangeStatusToFinished()
    {
        ChangeOrderStatusInvalidException.ThrowIfOrderStatusInvalidStepChange(
            Status,
            OrderStatus.Done,
            OrderStatus.Finished);

        Status = OrderStatus.Finished;
        return this;
    }

}