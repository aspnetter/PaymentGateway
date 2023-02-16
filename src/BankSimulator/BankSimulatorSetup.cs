using Microsoft.Extensions.DependencyInjection;

namespace BankSimulator;

public static class BankSimulatorSetup
{
    public static void AddBankSimulator(this IServiceCollection services)
    {
        services.AddHostedService<TransactionProcessor>();
    }
}