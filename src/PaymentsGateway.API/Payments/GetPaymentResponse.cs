using PaymentsGateway.Domain;

namespace PaymentsGateway.API.Payments;

public class GetPaymentResponse
{
    public Guid PaymentId { get; private set; }
    public decimal Amount { get; private set; }
    public string Currency { get; private set; }
    public string CardholderName { get; private set; }
    public string MaskedCardNumber { get; private set; }
    public string Status { get; private set; }
    public string StatusReason { get; private set; }
    public DateTime PaymentDateTimeUtc { get; private set; } 
    public GetPaymentResponse(Payment payment)
    {
        PaymentId = payment.Id;
        Amount = payment.TotalAmount.Total;
        Currency = payment.TotalAmount.Currency.Code;
        CardholderName = payment.Card.OwnerName;
        MaskedCardNumber = payment.Card.MaskedDetails;
        Status = payment.Result.Status.ToString().ToUpper();
        StatusReason = payment.Result.StatusReason;
        PaymentDateTimeUtc = payment.CreatedDateTimeUtc;
    }
}