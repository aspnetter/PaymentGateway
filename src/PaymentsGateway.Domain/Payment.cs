namespace PaymentsGateway.Domain;

public class Payment
{
    public Guid Id { get; private set; }
    
    public Guid MerchantId { get; private set; }
    
    public Amount TotalAmount { get; private set; }
    
    public CardDetails Card { get; private set; }
    
    public DateTime CreatedDateTimeUtc { get; private set; }
    
    public PaymentResult Result { get; set; }

    public Payment(Guid merchantId, Amount amount, CardDetails card)
    {
        Id = Guid.NewGuid();
        CreatedDateTimeUtc = DateTime.UtcNow;

        MerchantId = merchantId;
        TotalAmount = amount;
        Card = card;

        Result = new PaymentResult(PaymentStatusCode.Pending);
    }

    private Payment() {}
}