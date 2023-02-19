using FluentAssertions;
using PaymentsGateway.Domain;

namespace PaymentGateway.Domain.UnitTests
{
    public class ExpiryDateTests
    {
        [Theory]
        [InlineData(3, 2017, true)]
        [InlineData(-1, 2017, false)]
        [InlineData(13, 2017, false)]
        [InlineData(3, -1000, false)]
        public void Ctor_GivenMonthAndYear_ShouldOnlyAllowValuesThatFallWithinCorrectDateBoundaries(int month, int year, bool success)
        {
            var create = () => new ExpiryDate(month, year);

            if (success)
            {
                var date = create();

                date.Month.Should().Be(month);
                date.Year.Should().Be(year);
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