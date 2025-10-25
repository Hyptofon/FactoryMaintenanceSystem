using Domain.Entities;
using MediatR;

namespace Application.WorkOrders.Commands;

public record CancelWorkOrderCommand : IRequest<WorkOrder>
{
    public required Guid Id { get; init; }
}