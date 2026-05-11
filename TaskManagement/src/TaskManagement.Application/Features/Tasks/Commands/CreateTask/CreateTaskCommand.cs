using MediatR;
using TaskManagement.Domain.Enums;

namespace TaskManagement.Application.Features.Tasks.Commands.CreateTask;

public record CreateTaskCommand(
    string Title,
    string? Description,
    Priority Priority,
    DateTime? DueDate
) : IRequest<Guid>;
