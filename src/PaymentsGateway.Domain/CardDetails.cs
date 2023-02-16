using CreditCardValidator;

namespace PaymentsGateway.Domain;

public class CardDetails
{
    public string OwnerName { get; private set; }
    public string Number { get; private set; }
    public ExpiryDate Expires { get; private set; }
    public Cvv CvvCode { get; private set; }

    //TEST:UNIT
    public CardDetails(string? ownerName, string? cardNumber, int expiryMonth, int expiryYear, string? cvv)
    {
        if (string.IsNullOrWhiteSpace(ownerName))
        {
            throw new DomainException(GetType(), "Cardholder name cannot be empty");
        }

        if (!IsValidCardNumber(cardNumber))
        {
            throw new DomainException(GetType(), "Card number is not valid");
        }

        OwnerName = ownerName;
        Number = cardNumber;
        Expires = new ExpiryDate(expiryMonth, expiryYear);
        CvvCode = new Cvv(cvv);
    }
    
    private CardDetails() {}

    private static bool IsValidCardNumber(string? cardNumber)
    {
        if (string.IsNullOrWhiteSpace(cardNumber))
        {
            return false;
        }
        var detector = new CreditCardDetector(cardNumber);
        return detector.IsValid();
    }
}