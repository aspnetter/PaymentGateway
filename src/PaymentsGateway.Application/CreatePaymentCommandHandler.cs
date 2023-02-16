using BankSimulator.Interface;
using MediatR;
using PaymentsGateway.Domain;
using PaymentsGateway.Infrastructure;

namespace PaymentsGateway.Application;

public class CreatePaymentCommandHandler : IRequestHandler<CreatePaymentCommand, Guid>
{
    private readonly PaymentGatewayContext _context;
    private readonly IMediator _mediator;

    public CreatePaymentCommandHandler(PaymentGatewayContext context, IMediator mediator)
    {
        _context = context;
        _mediator = mediator;
    }
    
    public async Task<Guid> Handle(CreatePaymentCommand request, CancellationToken cancellationToken)
    {
        var payment = new Payment(
            request.MerchantId, 
            new Amount(request.Amount, request.CurrencyCode),
            new CardDetails(request.CardholderName,
                 request.CardNumber, 
                 request.ExpiryMonth, 
                 request.ExpiryYear,
                 request.Cvv)
            ); 

        await _context.Payments.AddAsync(payment, CancellationToken.None);

        await _context.SaveChangesAsync(CancellationToken.None);

        await _mediator.Send(new MakePaymentCommand(payment.Id, payment.Card.Number), CancellationToken.None);

        return payment.Id;
    }
}