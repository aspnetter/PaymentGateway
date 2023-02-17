using System.ComponentModel.DataAnnotations;
using MediatR;
using PaymentsGateway.Application.Validation;
using PaymentsGateway.Domain;
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace PaymentsGateway.Application;

public class CreatePaymentCommand: IRequest<Guid>, IValidatableObject
{
    [Required]
    public Guid MerchantId { get; set; }
    [Required, MinLength(4), MaxLength(64)]
    public string CardholderName { get; set; }
    [Required, CreditCard ]
    public string CardNumber { get; set; }
    [Required, Range(1, 12)]
    public int ExpiryMonth { get; set; }
    [Required]
    public int ExpiryYear { get; set; }
    [Required, Cvv(ErrorMessage = "CVV format is invalid")]
    public string Cvv { get; set; }
    [Required]
    public decimal Amount { get; set; }
    [Required, IsoCurrency]
    public string CurrencyCode { get; set; }
    public IEnumerable<ValidationResult> Validate(ValidationContext context)
    {
        var results = new List<ValidationResult>();

        var date = new ExpiryDate(ExpiryMonth, ExpiryYear);
        if (date.IsOverdue)
        {
            results.Add(new ValidationResult("Card is expired"));    
        }
        
        return results;
    }
}





