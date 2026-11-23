using Prometheus;

namespace DummyOrderApp.Api.Services;

public class OrderService
{
    private static readonly Counter ErrorCounter = Metrics
        .CreateCounter("order_processing_errors_total", "Total number of order processing errors");

    private static readonly Counter RequestCounter = Metrics
        .CreateCounter("order_requests_total", "Total number of order requests", labelNames: ["status"]);

    public OrderResult ProcessOrder(string? customerName, decimal amount)
    {
        try
        {
            // BUG: customerName null olabilir, null check yok
            var shortName = customerName.Substring(0, Math.Min(10, customerName.Length));

            RequestCounter.WithLabels("success").Inc();
            return new OrderResult(true, $"Order processed for {shortName}, amount: {amount:C}");
        }
        catch (Exception ex)
        {
            ErrorCounter.Inc();
            RequestCounter.WithLabels("error").Inc();
            throw new InvalidOperationException($"Order processing failed: {ex.Message}", ex);
        }
    }
}

public record OrderResult(bool Success, string Message);
