using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using PaymentsGateway.Application;

namespace PaymentsGateway.API.Payments;

[ApiController]
[Route("api/payments")]
public class PaymentsController : ControllerBase
{

    private readonly ILogger<PaymentsController> _logger;
    private readonly IMediator _mediator;

    public PaymentsController(ILogger<PaymentsController> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    [HttpGet("{paymentId}")]
    [ProducesResponseType(typeof(GetPaymentResponse), StatusCodes.Status200OK)]
    [ProducesErrorResponseType(typeof(NotFound))]
    public async Task<IActionResult> GetPaymentDetails(Guid paymentId)
    {
        var getPaymentQuery = new GetPaymentByIdQuery { Id = paymentId };
        var payment = await _mediator.Send(getPaymentQuery);

        if (payment != null)
        {
            var paymentResponse = new GetPaymentResponse(payment);
            return Ok(paymentResponse);
        }

        return NotFound();
    }

    [HttpPost("")]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    [ProducesErrorResponseType(typeof(BadRequest))]
    public async Task<IActionResult> CreatePayment([FromBody] CreatePaymentCommand сommand)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState); 
        }
        
        var newPaymentId = await _mediator.Send(сommand);
        
        return Created(new Uri($"{Request.Path}/{newPaymentId}", 
            UriKind.Relative), newPaymentId);
    }
}





