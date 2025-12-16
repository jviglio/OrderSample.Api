using System;
using System.Collections.Generic;
using System.Text;

namespace OrderSample.Application.Orders
{
    public class CancelOrderCommand
    {
        public Guid OrderId { get; }

        public CancelOrderCommand(Guid orderId)
        {
            OrderId = orderId;
        }
    }
}
