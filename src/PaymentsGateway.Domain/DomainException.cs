namespace PaymentsGateway.Domain;

public class DomainException: ApplicationException
{
    public DomainException(Type domainType, string message): 
        base($"{domainType.Name} - {message}")
    {
    }
}