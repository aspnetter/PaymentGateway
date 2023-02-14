using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace PaymentsGateway.API.Payments;

[ApiController]
[Route("api/payments")]
public class PaymentsController : ControllerBase
{

    private readonly ILogger<PaymentsController> _logger;

    public PaymentsController(ILogger<PaymentsController> logger)
    {
        _logger = logger;
    }

    [HttpGet("{paymentId}")]
    [ProducesResponseType(typeof(GetPaymentResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetPaymentDetails(Guid paymentId)
    {
        //var meetingDetails = await _meetingsModule.ExecuteQueryAsync(new GetMeetingDetailsQuery(meetingId));
        var paymentDetails = new GetPaymentResponse();
        return Ok(paymentDetails);
    }

    [HttpPost("")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> CreatePayment([FromBody] CreatePaymentRequest request)
    {
        //execute new CreatePaymentCommand
        return Ok();
    }
}





