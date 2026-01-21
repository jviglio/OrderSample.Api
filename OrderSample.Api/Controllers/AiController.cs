using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderSample.Application.Abstractions;

namespace OrderSample.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/ai")]
    public class AiController : ControllerBase
    {
        private readonly IAiClient _ai;

        public AiController(IAiClient ai)
        {
            _ai = ai;
        }

        [HttpPost("summarize")]
        public async Task<IActionResult> Summarize([FromBody] SummarizeRequest request, CancellationToken ct)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.Text))
                return BadRequest("Text is required.");

            var summary = await _ai.SummarizeAsync(request.Text, ct);
            return Ok(new { summary });
        }
    }

    public class SummarizeRequest
    {
        public string Text { get; set; }
    }
}
