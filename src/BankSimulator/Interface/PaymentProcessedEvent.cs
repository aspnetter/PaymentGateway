using MediatR;

namespace BankSimulator.Interface;

public class PaymentProcessedEvent : INotification
{
    public Guid PaymentId { get; }
    
    public string StatusCode { get; private set; }

    public string StatusReason { get; }

    public PaymentProcessedEvent(Guid paymentId, string statusCode, string statusReason = "")
    {
        PaymentId = paymentId;
        StatusCode = statusCode;
        StatusReason = statusReason;
    }
}