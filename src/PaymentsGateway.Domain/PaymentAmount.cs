namespace PaymentsGateway.Domain;

public class PaymentAmount
{
    public decimal Amount { get; private set; }
    public IsoCurrency Currency { get; private set; }

    //TEST:UNIT
    public PaymentAmount(decimal amount, string? currencyCode)
    {
        if (amount < 0)
        {
            throw new DomainException(GetType(), $"Invalid amount - {amount}");
        }
        
        Amount = amount;
        Currency = new IsoCurrency(currencyCode);
    }
}