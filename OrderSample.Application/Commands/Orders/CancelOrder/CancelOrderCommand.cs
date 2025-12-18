using System;

namespace OrderSample.Application.Commands.Orders.CancelOrder
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
