using System.Collections.Concurrent;

namespace BankSimulator;

public static class InMemoryStorage
{
    private static readonly ConcurrentQueue<BankCardTransaction> PaymentsToProcess = new();
    public static void Add(BankCardTransaction payment) => PaymentsToProcess.Enqueue(payment);
    public static IEnumerable<BankCardTransaction> GetPending()
    {
        while (PaymentsToProcess.TryDequeue(out var cardTransaction))
        {
            yield return cardTransaction;
        }
    }
}