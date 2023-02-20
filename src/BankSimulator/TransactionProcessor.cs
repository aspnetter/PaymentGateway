using BankSimulator.Interface;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BankSimulator;

public class TransactionProcessor : IHostedService, IDisposable
{
    private const int TransactionProcessIntervalSeconds = 20;
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private Timer? _timer;
    
    public TransactionProcessor(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }

    public Task StartAsync(CancellationToken stoppingToken)
    {
        _timer = new Timer(ProcessTransactionCallback, null, TimeSpan.Zero,
            TimeSpan.FromSeconds(TransactionProcessIntervalSeconds));

        return Task.CompletedTask;
    }

    private async void ProcessTransactionCallback(object? state)
    {
        var unprocessedTransactions = InMemoryStorage.GetPending();

        foreach (var cardTransaction in unprocessedTransactions)
        {
            await ProcessTransaction(cardTransaction);
        }
    }

    private async Task ProcessTransaction(BankCardTransaction transaction)
    {
        var paymentStatus = PaymentStatus.Fail;
        var paymentStatusReason = PaymentStatusReason.Unknown;
        var found = false;
 
        foreach (var (status, statusReasons) in TestData.Cards)
        {
            if (found) break;
            
            foreach (var cardNumbers in statusReasons)
            {
                var cards = cardNumbers.Value;

                if (cards.Any(c => string.CompareOrdinal(c, transaction.CardNumber) == 0))
                {
                    paymentStatus = status;
                    paymentStatusReason = cardNumbers.Key;
                    found = true;
                    break;
                }
            }
        }

        using var scope = _serviceScopeFactory.CreateScope();
        
        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
        await mediator.Publish(new PaymentProcessedEvent(
            transaction.PaymentId,
            paymentStatus.ToString(),
            paymentStatusReason.ToString()));
    }

    public Task StopAsync(CancellationToken stoppingToken)
    {
        _timer?.Change(Timeout.Infinite, 0);

        return Task.CompletedTask;
    }

    public void Dispose()
    {
        _timer?.Dispose();
    }
}