using MediatR;
using TaskManagement.Domain.Enums;

namespace TaskManagement.Application.Features.Tasks.Commands.UpdateTask;

public record UpdateTaskCommand(
    Guid Id,
    string Title,
    string? Description,
    Priority Priority,
    TaskItemStatus Status,
    DateTime? DueDate
) : IRequest<bool>;
