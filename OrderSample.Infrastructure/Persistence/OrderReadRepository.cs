using Microsoft.EntityFrameworkCore;
using OrderSample.Application.Abstractions;
using OrderSample.Application.Queries.Orders.GetOrders;
using OrderSample.Infrastructure.Persistence;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderSample.Infrastructure.Persistence
{
    public sealed class OrderReadRepository : IOrderReadRepository
    {
        private readonly OrderDbContext _context;

        public OrderReadRepository(OrderDbContext context)
        {
            _context = context;
        }

        public async Task<IReadOnlyList<OrderListItemDto>> GetAll()
        {
            return await _context.Orders
                .AsNoTracking()
                .Where(o => o.Status != Domain.Orders.OrderStatus.Cancelled) // opcional
                .Select(o => new OrderListItemDto(
                    o.Id,
                    o.Total.Amount,
                    o.Status.ToString()
                ))
                .ToListAsync();
        }
    }
}
