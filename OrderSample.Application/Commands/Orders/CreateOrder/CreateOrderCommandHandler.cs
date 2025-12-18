using OrderSample.Application.Abstractions;
using OrderSample.Domain.Orders;
using OrderSample.Domain.Common;
using System;
using System.Threading.Tasks;

namespace OrderSample.Application.Commands.Orders.CreateOrder
{
    public sealed class CreateOrderCommandHandler
    {
        private readonly IOrderRepository _repository;

        public CreateOrderCommandHandler(IOrderRepository repository)
        {
            _repository = repository;
        }

        public async Task<Guid> Handle(CreateOrderCommand command)
        {
            var orderId = Guid.NewGuid();
            var total = new Money(command.Total);

            var order = new Order(orderId, total);

            await _repository.Add(order);
            await _repository.SaveChanges();

            return order.Id;
        }
    }
}
