namespace PaymentsGateway.Domain;

public class ExpiryDate
{
    public int Month { get; private set; }
    public int Year { get; private set; }

    //TEST:UNIT
    public ExpiryDate(int month, int year)
    {
        var minDate = DateTime.MinValue;
        var maxDate = DateTime.MaxValue;
        
        if (month < minDate.Month || month > maxDate.Month)
        {
            throw new DomainException(GetType(), $"Month must be between {minDate.Month} and {maxDate.Month}");
        }

        if(year < minDate.Year || year > maxDate.Year)
        {
            throw new DomainException(GetType(), $"Year must be between {minDate.Year} and {maxDate.Year}");
        }
        
        Month = month;
        Year = year;
    }
    
    private ExpiryDate() {}
    
    //TEST:UNIT
    public bool IsOverdue
    {
        get
        {
            var today = DateOnly.FromDateTime(DateTime.UtcNow);
            return today <= LastDayOfMonth;
        }
    }

    private DateOnly LastDayOfMonth
    {
        get
        {
            var date = new DateOnly(Year, Month, 1);
            return date.AddMonths(1).AddDays(-1);
        }
    }
}