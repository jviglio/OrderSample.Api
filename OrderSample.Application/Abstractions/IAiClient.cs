using System.Threading;
using System.Threading.Tasks;

namespace OrderSample.Application.Abstractions
{

    public interface IAiClient
    {
        Task<string> SummarizeAsync(string text, CancellationToken ct);
    }
}

