using BankSimulator.Interface;

namespace BankSimulator;

public class BankCardTransaction
{
    public Guid Id { get; private set; }
    public Guid PaymentId { get; set; }
    public decimal RequestedAmount { get; private set; }
    public string RequestedAmountCurrency { get; private set; }
    public string CardNumber { get; set; }
    public string NameOnCard { get; set; }
    public DateOnly CardExpiry { get; set; }
    public string Cvv { get; set; }

    public BankCardTransaction(MakePaymentCommand command)
    {
        Id = new Guid();

        PaymentId = command.PaymentId;
        RequestedAmount = command.Amount;
        RequestedAmountCurrency = command.CurrencyCode ?? "USD";
        CardNumber = command.CardNumber;
        NameOnCard = command.CardholderName ?? "<INVALID_CARDHOLDER>";
        CardExpiry = new DateOnly(command.CardExpiryYear, command.CardExpiryMonth, 28);
        Cvv = command.Cvv ?? "<INVALID_CVV>";
    }
}