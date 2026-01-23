using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OrderSample.Api.Contracts.Webhooks;
using OrderSample.Application.Abstractions;

namespace OrderSample.Api.Controllers
{
    [ApiController]
    [Route("api/webhooks")]
    public class WebhooksController : ControllerBase
    {
        private readonly IN8nClient _n8nClient;

        public WebhooksController(IN8nClient n8nClient)
        {
            _n8nClient = n8nClient;
        }

        [HttpPost("ecommerce")]
        public async Task<IActionResult> Ecommerce([FromBody] EcommerceWebhookRequest request, CancellationToken ct)
        {
            // Payload "limpio" para n8n (solo lo que n8n necesita)
            var payload = new
            {
                message = request.Message,
                timestamp = request.Timestamp,
                messageId = request.MessageId,
                metadata = request.Metadata
            };

            var n8nResponse = await _n8nClient.SendEcommerceAsync(payload, ct);

            return Ok(new
            {
                ok = true,
                reply = n8nResponse
            });
        }
    }
}
