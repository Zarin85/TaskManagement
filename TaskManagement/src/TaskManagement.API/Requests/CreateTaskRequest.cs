using TaskManagement.Domain.Enums;

namespace TaskManagement.API.Requests;

public record CreateTaskRequest(
    string Title,
    string? Description,
    Priority Priority,
    DateTime? DueDate
);
