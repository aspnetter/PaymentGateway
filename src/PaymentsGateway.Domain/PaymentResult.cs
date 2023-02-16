namespace PaymentsGateway.Domain;

public class PaymentResult
{
    public PaymentStatus Status { get; private set; }
    public string StatusCode { get; private set; }

    public PaymentResult(PaymentStatus status, string statusCode)
    {
        Status = status;
        StatusCode = statusCode;
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