using System.Text.RegularExpressions;

namespace PaymentsGateway.Domain;

public partial class Cvv
{
    public string Code { get; private set; }
    //TEST:UNIT
    public Cvv(string? code)
    {
        if (string.IsNullOrWhiteSpace(code))
        {
            throw new DomainException(GetType(), "CVV cannot be empty");
        }

        if (!MyRegex().IsMatch(code))
        {
            throw new DomainException(GetType(), "Invalid CVV code format");
        }

        Code = code;
    }
    
    private Cvv() {}
    
    [GeneratedRegex("^[0-9]{3,4}$")]
    private static partial Regex MyRegex();
}