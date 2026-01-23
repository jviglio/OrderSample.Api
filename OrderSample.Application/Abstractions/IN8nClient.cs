using System.Threading;
using System.Threading.Tasks;

namespace OrderSample.Application.Abstractions
{
    public interface IN8nClient
    {
        Task<string> SendEcommerceAsync(object request, CancellationToken ct);
    }
}