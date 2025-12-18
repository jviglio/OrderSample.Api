using OrderSample.Application.Abstractions;
using OrderSample.Domain.Orders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using OrderSample.Infrastructure.Persistence;

public class OrderRepository : IOrderRepository
{
    private readonly OrderDbContext _context;

    public OrderRepository(OrderDbContext context)
    {
        _context = context;
    }

    public async Task<Order?> GetById(Guid id)
    {
        return await _context.Orders
            .SingleOrDefaultAsync(o => o.Id == id);
    }

    public async Task Add(Order order)
    {
        await _context.Orders.AddAsync(order);
    }

    public async Task SaveChanges()
    {
        await _context.SaveChangesAsync();
    }
}
