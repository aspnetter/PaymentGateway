namespace PaymentsGateway.Domain;

public class Payment
{
    public Guid Id { get; }
    
    public Guid MerchantId { get; }
    
    public PaymentAmount TotalAmount { get; }
    
    public CardDetails Card { get; private set; }
    
    public DateTime CreatedDateTimeUtc { get; private set; }
    
    public PaymentResult Result { get; private set; }

    public Payment(Guid merchantId, PaymentAmount amount, CardDetails card)
    {
        Id = Guid.NewGuid();
        CreatedDateTimeUtc = DateTime.UtcNow;

        MerchantId = merchantId;
        TotalAmount = amount;
        Card = card;
        
        //TODO: publish event
    }
}