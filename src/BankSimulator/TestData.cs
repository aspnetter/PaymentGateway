namespace BankSimulator;

public static class TestData
{
    public static readonly Dictionary<PaymentStatus, Dictionary<PaymentStatusReason, IEnumerable<string>>> Cards;
    
    static TestData()
    {
        Cards = new()
        {
            { PaymentStatus.Success, SuccessCards },
            { PaymentStatus.Fail, FailedCards },
            { PaymentStatus.Error, ErrorCards }
        };
    }
    
    private static readonly Dictionary<PaymentStatusReason, IEnumerable<string>> SuccessCards = new()
        {
            { PaymentStatusReason.AuthPassed,  new[]
            {
                "4195678753433085", // Visa
                "5426078842522527", // Mastercard
                "376028410137308", // AMEX
            } },
            { PaymentStatusReason.AuthPassed3Ds,  new[]
            {
                "4195701139986772", //Visa
            } },
        };
    
    private static readonly Dictionary<PaymentStatusReason, IEnumerable<string>> FailedCards = new()
        {
            { PaymentStatusReason.AuthFailed,  new[]
            {
                "5480419659666929", // Mastercard 
                "4195674145080866", // Visa
            } },
            { PaymentStatusReason.AuthFailed3Ds,  new[]
            {
                "5500056579406915", // Mastercard 
                "4195702673009815", // Visa
            } },
            { PaymentStatusReason.CardholderInvalid,  new[]
            {
                "5506192477931623", // Mastercard 
                "4195658884693492", // Visa, 
            } },
            { PaymentStatusReason.InsufficientFunds,  new[]
            {
                "5549877477014359", // Mastercard",
                "4195676581656695", // Visa
            } },
        };
    
    private static readonly Dictionary<PaymentStatusReason, IEnumerable<string>> ErrorCards = new()
        {
            { PaymentStatusReason.ErrorSystem,  new[]
            {
                "5265003394168788", // Mastercard 
                "4195671343985714", // Visa" 
            } }
        };
}

