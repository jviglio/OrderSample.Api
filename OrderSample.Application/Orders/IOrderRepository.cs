using OrderSample.Domain.Orders;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrderSample.Application.Orders
{
    public interface IOrderRepository
    {
        Order GetById(Guid id);
        IEnumerable<Order> GetAll();
        void Add(Order order);
        void Save(Order order);
    }
}
