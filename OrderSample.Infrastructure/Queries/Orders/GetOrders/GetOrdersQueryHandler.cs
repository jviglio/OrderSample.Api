using Microsoft.EntityFrameworkCore;
using OrderSample.Infrastructure.Persistence;
using OrderSample.Application.Queries.Orders.GetOrders;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderSample.Infrastructure.Queries.Orders.GetOrders
{
    public sealed class GetOrdersQueryHandler
    {
        private readonly OrderDbContext _context;

        public GetOrdersQueryHandler(OrderDbContext context)
        {
            _context = context;
        }

        public async Task<IReadOnlyList<OrderListItemDto>> Handle()
        {
            return await _context.Orders
                .AsNoTracking()
                .Select(o => new OrderListItemDto(
                    o.Id,
                    o.Total.Amount,
                    o.Status.ToString()
                ))
                .ToListAsync();
        }
    }
}
