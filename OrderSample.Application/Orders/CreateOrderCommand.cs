using System;
using System.Collections.Generic;
using System.Text;

namespace OrderSample.Application.Orders
{
    public class CreateOrderCommand
    {
        public decimal Total { get; }

        public CreateOrderCommand(decimal total)
        {
            Total = total;
        }
    }
}
