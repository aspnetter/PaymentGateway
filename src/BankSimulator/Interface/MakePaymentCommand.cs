using MediatR;

namespace BankSimulator.Interface;

public class MakePaymentCommand : IRequest<bool>
{
    public Guid PaymentId { get; }
    public string CardNumber { get; private set; }
    public decimal Amount { get; private set; }
    public string? CurrencyCode { get; private set; }
    public string? CardholderName { get; private set; }
    public int CardExpiryYear { get; private set; }
    public int CardExpiryMonth { get; private set; }
    public string? Cvv { get; private set; }

    public MakePaymentCommand(Guid paymentId, string cardNumber, 
        decimal amount, string currencyCode, string cardHolderName,
        int expiryYear, int expiryMonth, string cvv)
    {
        PaymentId = paymentId;
        CardNumber = cardNumber;
        Amount = amount;
        CurrencyCode = currencyCode;
        CardholderName = cardHolderName;
        CardExpiryYear = expiryYear;
        CardExpiryMonth = expiryMonth;
        Cvv = cvv;
    }
}