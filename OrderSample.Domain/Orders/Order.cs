using OrderSample.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrderSample.Domain.Orders
{
    public class Order : Entity<Guid>
    {
        public Money Total { get; private set; }
        public OrderStatus Status { get; private set; }

        private Order() { }

        public Order(Guid id, Money total)
        {
            Id = id;
            Total = total;
            Status = OrderStatus.Created;
        }

        public void Cancel()
        {
            if (Status == OrderStatus.Cancelled)
                throw new InvalidOperationException("Order already cancellled");

            Status = OrderStatus.Cancelled;
        }
    }
}
