using Domain.Entities;
using MediatR;

namespace Application.WorkOrders.Commands;

public record StartWorkOrderCommand : IRequest<WorkOrder>
{
    public required Guid Id { get; init; }
}