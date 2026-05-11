using TaskManagement.Domain.Common;
using TaskManagement.Domain.Enums;

namespace TaskManagement.Domain.Entities;

public class TaskItem : BaseEntity
{
    public string Title { get; private set; } = default!;
    public string? Description { get; private set; }
    public TaskItemStatus Status { get; private set; }
    public Priority Priority { get; private set; }
    public DateTime? DueDate { get; private set; }

    private TaskItem() { }

    public static TaskItem Create(string title, string? description, Priority priority, DateTime? dueDate)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(title);

        return new TaskItem
        {
            Id = Guid.NewGuid(),
            Title = title.Trim(),
            Description = description?.Trim(),
            Status = TaskItemStatus.Todo,
            Priority = priority,
            DueDate = dueDate,
            CreatedAt = DateTime.UtcNow
        };
    }

    public void Update(string title, string? description, Priority priority, DateTime? dueDate)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(title);

        Title = title.Trim();
        Description = description?.Trim();
        Priority = priority;
        DueDate = dueDate;
        UpdatedAt = DateTime.UtcNow;
    }

    public void ChangeStatus(TaskItemStatus newStatus)
    {
        Status = newStatus;
        UpdatedAt = DateTime.UtcNow;
    }
}
