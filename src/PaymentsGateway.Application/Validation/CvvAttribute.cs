using System.ComponentModel.DataAnnotations;
using PaymentsGateway.Domain;

namespace PaymentsGateway.Application.Validation;

public class CvvAttribute: ValidationAttribute
{
    public override bool IsValid(object? value)
    {
        var code = value as string;
        
        try
        {
            var unused = new Cvv(code);
        }
        catch (DomainException e)
        {
            ErrorMessage = e.Message;
            return false;
        }

        return true;
    }
}                                                                            