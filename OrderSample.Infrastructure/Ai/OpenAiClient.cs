using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using OrderSample.Application.Abstractions;

namespace OrderSample.Infrastructure.Ai
{
    public sealed class OpenAiClient : IAiClient
    {
        private readonly HttpClient _http;
        private readonly string _model;

        public OpenAiClient(HttpClient http, string model)
        {
            _http = http;
            _model = model;
        }

        public async Task<string> SummarizeAsync(string text, CancellationToken ct = default)
        {
            ct.ThrowIfCancellationRequested();

            var payload = new
            {
                model = _model,
                messages = new object[]
                {
                    new { role = "system", content = "You are a helpful assistant. Answer the user's question clearly." },
                    new { role = "user", content = text }
                }
            };

            using var content = new StringContent(
                JsonSerializer.Serialize(payload),
                Encoding.UTF8,
                "application/json"
            );

            using var resp = await _http.PostAsync("chat/completions", content, ct);

            var body = await resp.Content.ReadAsStringAsync();

            if (!resp.IsSuccessStatusCode)
                throw new InvalidOperationException(
                    $"OpenAI error: {(int)resp.StatusCode} - {body}"
                );

            using var doc = JsonDocument.Parse(body);

            var summary = doc.RootElement
                .GetProperty("choices")[0]
                .GetProperty("message")
                .GetProperty("content")
                .GetString();

            return summary!.Trim();
        }


        public static void ConfigureHttpClient(HttpClient http, string baseUrl, string apiKey)
        {
            http.BaseAddress = new Uri(baseUrl);
            http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);
        }
    }
}