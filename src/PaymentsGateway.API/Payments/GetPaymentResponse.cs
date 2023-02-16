using PaymentsGateway.Domain;

namespace PaymentsGateway.API.Payments;

public class GetPaymentResponse
{
    public Guid PaymentId { get; private set; }
    public DateTime PaymentDateTimeUtc { get; private set; } 
    public decimal Amount { get; private set; }
    public string Currency { get; private set; }
    public string CardholderName { get; private set; }
    public string MaskedCardNumber { get; private set; }
    public PaymentStatus Status { get; private set; }
    public string Result { get; private set; }
    public GetPaymentResponse(Payment payment)
    {
        throw new NotImplementedException();
    }
}