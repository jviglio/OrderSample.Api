using System;

namespace OrderSample.Application.Queries.Orders.GetOrders
{
    public sealed class OrderListItemDto
    {
        public Guid Id { get; }
        public decimal Total { get; }
        public string Status { get; }

        public OrderListItemDto(Guid id, decimal total, string status)
        {
            Id = id;
            Total = total;
            Status = status;
        }
    }
}
