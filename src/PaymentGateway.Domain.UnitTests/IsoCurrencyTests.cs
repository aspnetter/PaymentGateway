using FluentAssertions;
using PaymentsGateway.Domain;

namespace PaymentGateway.Domain.UnitTests
{
    public class IsoCurrencyTests
    {
        [Theory]
        [InlineData("ZAR", true)]
        [InlineData(null, false)]
        [InlineData("", false)]
        [InlineData("GOLD", false)]
        public void Ctor_GivenCurrency_ShouldOnlyAllowValidCurrency(string currencyCode, bool success)
        {
            var create = () => new IsoCurrency(currencyCode);

            if (success)
            {
                var isoCurrency = create();

                isoCurrency.Code.Should().Be(currencyCode);
            }
            else
            {
                create.Should().Throw<DomainException>();
            }
        }

        [Theory]
        [InlineData(3, 2017, true)]
        [InlineData(3, 2018, true)]
        [InlineData(2, 2017, false)]
        [InlineData(3, 2016, false)]
        public void IsOverdue_GivenMonthAndYear_ShouldOnlyAllowValuesThatRepresentDateInCurrentMonthOrInTheFuture(int month, int year, bool success)
        {
            var date = new ExpiryDate(month, year);
            date.IsOverdue(new DateTime(2017, 3, 1)).Should().Be(success);
        }
    }
}