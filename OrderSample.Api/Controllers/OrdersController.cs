using Microsoft.AspNetCore.Mvc;
using OrderSample.Application.Orders;
using System;
using System.Linq;

namespace OrderSample.Api.Controllers
{
    [ApiController] 
    [Route("orders")]
    public class OrdersController : ControllerBase
    {
        private readonly OrderService _service;

        public OrdersController(OrderService service)
        {
            _service = service;
        }

        [HttpPost]
        public IActionResult Create([FromQuery] decimal total)
        {
            var id = _service.Create(new CreateOrderCommand(total));
            return Ok(id);
        }

        [HttpPost("{id}/cancel")]
        public IActionResult Cancel(Guid id)
        {
            _service.Cancel(new CancelOrderCommand(id));
            return Ok();
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var orders = _service.GetAll()
                    .Select(o => new
                    {
                        Id = o.Id,
                        Total = o.Total.Amount,
                        Status = o.Status.ToString()
                    });

            return Ok(orders);
        }
    }
}
