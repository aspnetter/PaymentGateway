namespace PaymentsGateway.Domain;

public class Amount
{
    public decimal Total { get; private set; }
    public IsoCurrency Currency { get; private set; }

    //TEST:UNIT
    public Amount(decimal total, string? currencyCode)
    {
        if (total < 0)
        {
            throw new DomainException(GetType(), $"Invalid amount - {total}");
        }
        
        Total = total;
        Currency = new IsoCurrency(currencyCode);
    }
    
    private Amount() {}
}