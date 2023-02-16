using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.DependencyInjection;

namespace PaymentsGateway.Infrastructure;

public class PaymentGatewayContextFactory : IDesignTimeDbContextFactory<PaymentGatewayContext>
{
    public PaymentGatewayContext CreateDbContext(string[] args)
    {
        var serviceCollection = new ServiceCollection();
        
        PaymentGatewayContext.Init(serviceCollection , "Server=127.0.0.1;Port=5532;Database=paymentgateway;User Id=postgres;Password=postgres;");

        return serviceCollection .BuildServiceProvider().GetService<PaymentGatewayContext>()!;
    }
}