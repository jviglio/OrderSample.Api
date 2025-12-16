using Microsoft.EntityFrameworkCore;
using OrderSample.Domain.Orders;

namespace OrderSample.Infrastructure.Persistence
{
    public class OrderDbContext : DbContext
    {
        public DbSet<Order> Orders { get; set; }

        public OrderDbContext(DbContextOptions<OrderDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>(builder =>
            {
                builder.HasKey(o => o.Id);

                builder.OwnsOne(o => o.Total, money =>
                {
                    money.Property(m => m.Amount);
                });
            });
        }

    }
}
