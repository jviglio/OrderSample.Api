using OrderSample.Domain.Common;
using OrderSample.Domain.Orders;
using System;
using System.Collections.Generic;

namespace OrderSample.Application.Orders
{
    public class OrderService
    {
        private readonly IOrderRepository _repository;

        public OrderService(IOrderRepository repository)
        {
            _repository = repository;
        }

        public Guid Create(CreateOrderCommand command)
        {
            var order = new Order(Guid.NewGuid(), new Money(command.Total));
            _repository.Add(order);
            return order.Id;
        }

        public void Cancel(CancelOrderCommand command)
        {
            var order = _repository.GetById(command.OrderId);
            order.Cancel();
            _repository.Save(order);
        }

        public IEnumerable<Order> GetAll()
        {
            return _repository.GetAll();
        }
    }
}
