using OrderSample.Application.Abstractions;
using System;
using System.Threading.Tasks;

namespace OrderSample.Application.Commands.Orders.CancelOrder
{
    public sealed class CancelOrderCommandHandler
    {
        private readonly IOrderRepository _repository;

        public CancelOrderCommandHandler(IOrderRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(CancelOrderCommand command)
        {
            var order = await _repository.GetById(command.OrderId);

            if (order == null)
                throw new InvalidOperationException("Order not found");

            order.Cancel();

            await _repository.SaveChanges();
        }
    }
}
