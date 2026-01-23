using System;

namespace OrderSample.Api.Contracts.Webhooks

{
    public sealed class EcommerceWebhookRequest
    {
        public string Message { get; set; } = default!;
        public DateTime Timestamp { get; set; }
        public string MessageId { get; set; } = default!;
        public Metadata Metadata { get; set; } = new Metadata();
    }

    public sealed class Metadata
    {
        public string Website { get; set; } = "";
        public string Source { get; set; } = "";
        public string PageUrl { get; set; } = "";
        public string SessionId { get; set; } = "";
        public int MessageCount { get; set; }
    }
}