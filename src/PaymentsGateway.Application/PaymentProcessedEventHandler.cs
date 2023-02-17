using BankSimulator.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PaymentsGateway.Domain;
using PaymentsGateway.Infrastructure;

namespace PaymentsGateway.Application;

public class PaymentProcessedEventHandler:  INotificationHandler<PaymentProcessedEvent>
{
    private readonly PaymentGatewayContext _context;
    
    public PaymentProcessedEventHandler(PaymentGatewayContext context)
    {
        _context = context;
    }
    
    public async Task Handle(PaymentProcessedEvent notification, CancellationToken cancellationToken)
    {
        var payment  = await _context.Payments.FirstOrDefaultAsync(
            x => x.Id == notification.PaymentId, 
            cancellationToken);
        
        payment.Result = new PaymentResult(notification.StatusCode, notification.StatusReason); 
        
        await _context.SaveChangesAsync(cancellationToken);
    }
}