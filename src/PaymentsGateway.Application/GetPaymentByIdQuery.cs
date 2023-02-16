using MediatR;
using PaymentsGateway.Domain;

namespace PaymentsGateway.Application;

public class GetPaymentByIdQuery: IRequest<Payment>
{
    public Guid Id { get; set; }
}