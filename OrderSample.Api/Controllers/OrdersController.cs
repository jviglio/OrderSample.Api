using Microsoft.AspNetCore.Mvc;
using OrderSample.Application.Commands.Orders.CreateOrder;
using OrderSample.Application.Commands.Orders.CancelOrder;
using System;
using System.Threading.Tasks;
using OrderSample.Application.Queries.Orders.GetOrders;
using Microsoft.AspNetCore.Authorization;

namespace OrderSample.Api.Controllers
{
    [ApiController]
    [Route("orders")]
    public class OrdersController : ControllerBase
    {
        private readonly CreateOrderCommandHandler _createOrderHandler;
        private readonly CancelOrderCommandHandler _cancelOrderHandler;
        private readonly GetOrdersQueryHandler _getOrdersHandler;

        public OrdersController(
            CreateOrderCommandHandler createOrderHandler,
            CancelOrderCommandHandler cancelOrderHandler,
            GetOrdersQueryHandler getOrdersHandler)
        {
            _createOrderHandler = createOrderHandler;
            _cancelOrderHandler = cancelOrderHandler;
            _getOrdersHandler = getOrdersHandler;
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromQuery] decimal total)
        {
            var command = new CreateOrderCommand(total);
            var id = await _createOrderHandler.Handle(command);

            return CreatedAtAction(nameof(Create), new { id }, null);
        }

        [HttpPost("{id}/cancel")]
        public async Task<IActionResult> Cancel(Guid id)
        {
            var command = new CancelOrderCommand(id);
            await _cancelOrderHandler.Handle(command);

            return Ok();
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var orders = await _getOrdersHandler.Handle();
            return Ok(orders);
        }

    }
}
