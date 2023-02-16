using BankSimulator.Interface;
using MediatR;

namespace BankSimulator;

public class MakePaymentCommandHandler: IRequestHandler<MakePaymentCommand, bool>
{
    public Task<bool> Handle(MakePaymentCommand makePaymentRequest, CancellationToken cancellationToken)
    {
        var cardTransaction = new BankCardTransaction(makePaymentRequest);
        InMemoryStorage.Add(cardTransaction);

        return Task.FromResult(true);
    }
}