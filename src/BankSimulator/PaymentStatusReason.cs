namespace BankSimulator;

public enum PaymentStatusReason
{
    AuthPassed,
    AuthPassed3Ds,
    AuthFailed,
    AuthFailed3Ds,
    CardholderInvalid,
    InsufficientFunds,
    ErrorSystem,
    Unknown
}