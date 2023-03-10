using System.ComponentModel.DataAnnotations;
using PaymentsGateway.Domain;

namespace PaymentsGateway.Application.Validation;

public class IsoCurrencyAttribute : ValidationAttribute
{
    public override bool IsValid(object? value)
    {
        if (value == null)
        {
            return false;
        }
        try
        {
            var currencyCode = (string) value;
            var unused = new IsoCurrency(currencyCode);
        }
        catch (DomainException e)
        {
            ErrorMessage = e.Message;
            return false;
        }

        return true;
    }
}