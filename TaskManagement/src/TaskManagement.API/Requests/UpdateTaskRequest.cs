using TaskManagement.Domain.Enums;

namespace TaskManagement.API.Requests;

public record UpdateTaskRequest(
    string Title,
    string? Description,
    Priority Priority,
    TaskItemStatus Status,
    DateTime? DueDate
);
