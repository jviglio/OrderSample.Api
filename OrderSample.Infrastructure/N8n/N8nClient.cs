using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using OrderSample.Application.Abstractions;

namespace OrderSample.Infrastructure.N8n
{
    public sealed class N8nClient : IN8nClient
    {
        private readonly HttpClient _http;

        public N8nClient(HttpClient http)
        {
            _http = http;
        }

        public async Task<string> SendEcommerceAsync(object request, CancellationToken ct)
        {
            var json = JsonSerializer.Serialize(request);
            using var content = new StringContent(json, Encoding.UTF8, "application/json");

            var resp = await _http.PostAsync("webhook/agenteinmobiliario", content, ct);
            resp.EnsureSuccessStatusCode();

            return await resp.Content.ReadAsStringAsync();
        }
    }
}