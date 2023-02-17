namespace PaymentsGateway.Domain;

public class PaymentResult
{
    public PaymentStatusCode Status { get; private set; }
    public string StatusReason { get; private set; }
    public bool IsSuccessful => Status == PaymentStatusCode.Success;

    public PaymentResult(PaymentStatusCode status, string statusReason = "")
    {
        Status = status;
        StatusReason = statusReason;
    }
    public PaymentResult(string status, string statusReason)
    {
        Status = PaymentStatusCode.Unknown;
        if (Enum.TryParse(status, out PaymentStatusCode knownStatus))
        {
            Status = knownStatus;
        }
        StatusReason = statusReason;
    }
    private PaymentResult() {}
}