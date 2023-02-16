namespace PaymentsGateway.Domain;

public class IsoCurrency
{
    public string? Code { get; private set; }
    
    //TEST:UNIT
    public IsoCurrency(string? code)
    {
        if (string.IsNullOrWhiteSpace(code))
        {
            throw new DomainException(GetType(), "Currency code cannot be empty");
        }

        var codeExists = ISO._4217.CurrencyCodesResolver.Codes
            .Any(c => c.Code == code);
        if (!codeExists)
        {
            throw new DomainException(GetType(), $"Currency code {code} is not a valid ISO code");
        }
        
        Code = code;
    }
    
    private IsoCurrency() {}
}