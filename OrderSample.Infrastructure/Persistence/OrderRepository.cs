using System;
using System.Collections.Generic;
using System.Linq;
using OrderSample.Application.Orders;
using OrderSample.Domain.Orders;

namespace OrderSample.Infrastructure.Persistence
{
    public class OrderRepository : IOrderRepository
    {
        private readonly OrderDbContext _context;

        public OrderRepository(OrderDbContext context)
        {
            _context = context;
        }

        public Order GetById(Guid id)
        {
            return _context.Orders.Single(o => o.Id == id);
        }

        public IEnumerable<Order> GetAll()
        {
            return _context.Orders.ToList();
        }

        public void Add(Order order)
        {
            _context.Orders.Add(order);
            _context.SaveChanges();
        }

        public void Save(Order order)
        {
            _context.SaveChanges();
        }
    }
}
