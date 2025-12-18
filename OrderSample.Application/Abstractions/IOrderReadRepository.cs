using OrderSample.Application.Queries.Orders.GetOrders;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrderSample.Application.Abstractions
{
    public interface IOrderReadRepository
    {
        Task<IReadOnlyList<OrderListItemDto>> GetAll();
    }
}
