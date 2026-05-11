using TaskManagement.Domain.Enums;

namespace TaskManagement.Application.Common.Models;

public record TaskDto(
    Guid Id,
    string Title,
    string? Description,
    TaskItemStatus Status,
    Priority Priority,
    DateTime? DueDate,
    DateTime CreatedAt,
    DateTime? UpdatedAt
);
