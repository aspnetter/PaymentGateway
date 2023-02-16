using MediatR;
using PaymentsGateway.Domain;
using PaymentsGateway.Infrastructure;

namespace PaymentsGateway.Application;

public class CreatePaymentCommandHandler : IRequestHandler<CreatePaymentCommand, Guid>
{
    private readonly PaymentGatewayContext _context;

    public CreatePaymentCommandHandler(PaymentGatewayContext context)
    {
        _context = context;
    }
    
    public async Task<Guid> Handle(CreatePaymentCommand request, CancellationToken cancellationToken)
    {
        var payment = new Payment(
            request.MerchantId, 
            new PaymentAmount(request.Amount, request.CurrencyCode),
            new CardDetails(request.CardholderName,
                 request.CardNumber, 
                 request.ExpiryMonth, 
                 request.ExpiryYear,
                 request.Cvv)
            ); 

        await _context.Payments.AddAsync(payment, CancellationToken.None);

        await _context.SaveChangesAsync(CancellationToken.None);

        return payment.Id;
    }
}