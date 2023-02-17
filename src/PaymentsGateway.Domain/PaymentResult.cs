namespace PaymentsGateway.Domain;

public class PaymentResult
{
    public PaymentStatus Status { get; private set; }
    public string StatusReason { get; private set; }

    public PaymentResult(PaymentStatus status, string statusReason)
    {
        Status = status;
        StatusReason = statusReason;
    }
    
    private PaymentResult() {}

    public bool IsSuccessful
    {
        get
        {
            return Status == PaymentStatus.Success;
        }
    }
}