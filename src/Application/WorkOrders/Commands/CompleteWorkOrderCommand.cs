using Domain.Entities;
using MediatR;

namespace Application.WorkOrders.Commands;

public record CompleteWorkOrderCommand : IRequest<WorkOrder>
{
    public required Guid Id { get; init; }
    public required string CompletionNotes { get; init; }
}