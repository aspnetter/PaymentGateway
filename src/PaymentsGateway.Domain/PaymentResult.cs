namespace PaymentsGateway.Domain;

public class PaymentResult
{
    public PaymentStatus Status { get; private set; }
    public string PaymentStatusCode { get; private set; }

    public bool IsSuccessful
    {
        get
        {
            return Status == PaymentStatus.Success;
        }
    }
}