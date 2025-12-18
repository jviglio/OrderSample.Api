using OrderSample.Domain.Orders;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OrderSample.Application.Abstractions
{
    public interface IOrderRepository
    {
        Task Add(Order order);
        Task<Order?> GetById(Guid id);
        Task SaveChanges();
    }
}
