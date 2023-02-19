using FluentAssertions;
using PaymentsGateway.Domain;

namespace PaymentGateway.Domain.UnitTests;

public class PaymentTests
{
    [Fact]
    public void Ctor_GivenCorrectAmounts_ShouldCreatePaymentWithPendingState()
    {
        var payment = new Payment(Guid.NewGuid(), new Amount(100, "GBP"),
            new CardDetails("Kate", "5500056579406915", 1, 2050, "831"));

        payment.Result.Status.Should().Be(PaymentStatusCode.Pending);
    }
}