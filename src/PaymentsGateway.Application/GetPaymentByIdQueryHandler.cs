using MediatR;
using Microsoft.EntityFrameworkCore;
using PaymentsGateway.Domain;
using PaymentsGateway.Infrastructure;

namespace PaymentsGateway.Application;

public class GetPaymentByIdQueryHandler: IRequestHandler<GetPaymentByIdQuery, Payment>
{
    private readonly PaymentGatewayContext _context;

    public GetPaymentByIdQueryHandler(PaymentGatewayContext context)
    {
        _context = context;
    }

    public async Task<Payment> Handle(GetPaymentByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await _context.Payments.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        return result;
    }
}
