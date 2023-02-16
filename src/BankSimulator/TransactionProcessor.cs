using BankSimulator.Interface;
using MediatR;
using Microsoft.Extensions.Hosting;

namespace BankSimulator;

public class TransactionProcessor : IHostedService, IDisposable
{
    private const int TransactionProcessIntervalSeconds = 30;
    private readonly IMediator _mediator;
    private Timer? _timer = null;

    public TransactionProcessor(IMediator mediator)
    {
        _mediator = mediator;
    }

    public Task StartAsync(CancellationToken stoppingToken)
    {
        _timer = new Timer(ProcessTransactionCallback, null, TimeSpan.Zero,
            TimeSpan.FromSeconds(TransactionProcessIntervalSeconds));

        return Task.CompletedTask;
    }

    private void ProcessTransactionCallback(object? state)
    {
        var unprocessedTransactions = InMemoryStorage.GetPending();

        foreach (var cardTransaction in unprocessedTransactions)
        {
            ProcessTransaction(cardTransaction);
        }

    }

    private void ProcessTransaction(BankCardTransaction transaction)
    {
        //TODO decide successful & unsuccessful cards

        _mediator.Publish(new PaymentProcessedEvent(transaction.PaymentId, "DECLINED", "INSUFFICIENT_FUNDS")); 
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