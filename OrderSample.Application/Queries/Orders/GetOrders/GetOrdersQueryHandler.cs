using OrderSample.Application.Abstractions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrderSample.Application.Queries.Orders.GetOrders
{
    public sealed class GetOrdersQueryHandler
    {
        private readonly IOrderReadRepository _readRepository;

        public GetOrdersQueryHandler(IOrderReadRepository readRepository)
        {
            _readRepository = readRepository;
        }

        public Task<IReadOnlyList<OrderListItemDto>> Handle()
        {
            return _readRepository.GetAll();
        }
    }
}
