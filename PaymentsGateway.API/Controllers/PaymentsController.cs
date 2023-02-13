using Microsoft.AspNetCore.Mvc;

namespace PaymentsGateway.API.Controllers;

[ApiController]
[Route("[controller]")]
public class PaymentsController : ControllerBase
{

    private readonly ILogger<PaymentsController> _logger;

    public PaymentsController(ILogger<PaymentsController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "payment")]
    public IEnumerable<object> Get()
    {
        return null;
    }
}

