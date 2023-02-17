using BankSimulator.Interface;
using MediatR;
using Microsoft.Extensions.Hosting;

namespace BankSimulator;

public class TransactionProcessor : IHostedService, IDisposable
{
    private const int TransactionProcessIntervalSeconds = 10;
    private readonly IMediator _mediator;
    private Timer? _timer;

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
        var paymentStatus = PaymentStatus.Fail;
        var paymentStatusReason = PaymentStatusReason.Unknown;
        var found = false;
 
        foreach (var statusReasons in TestData.Cards)
        {
            if (found) break;
            var status = statusReasons.Key;
            foreach (var reasons in statusReasons.Value)
            {
                var cards = reasons.Value;

                if (cards.Any(c => string.CompareOrdinal(c, transaction.CardNumber) == 0))
                {
                    paymentStatus = status;
                    paymentStatusReason = reasons.Key;
                    found = true;
                    break;
                }
            }
        }

        _mediator.Publish(new PaymentProcessedEvent(
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