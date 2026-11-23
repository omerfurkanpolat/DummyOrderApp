using DummyOrderApp.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace DummyOrderApp.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrderController : ControllerBase
{
    private readonly OrderService _orderService;
    private readonly ILogger<OrderController> _logger;

    public OrderController(OrderService orderService, ILogger<OrderController> logger)
    {
        _orderService = orderService;
        _logger = logger;
    }

    [HttpPost("process")]
    public IActionResult ProcessOrder([FromBody] OrderRequest request)
    {
        try
        {
            var result = _orderService.ProcessOrder(request.CustomerName, request.Amount);
            _logger.LogInformation("Order processed successfully for customer: {CustomerName}", request.CustomerName);
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Order processing failed. CustomerName={CustomerName}", request.CustomerName);
            return StatusCode(500, new { error = ex.Message });
        }
    }

    /// <summary>
    /// customerName null göndererek NullReferenceException tetikler
    /// </summary>
    [HttpPost("trigger-bug")]
    public IActionResult TriggerBug()
    {
        return ProcessOrder(new OrderRequest { CustomerName = null, Amount = 99.99m });
    }
}

public class OrderRequest
{
    public string? CustomerName { get; set; }
    public decimal Amount { get; set; }
}
