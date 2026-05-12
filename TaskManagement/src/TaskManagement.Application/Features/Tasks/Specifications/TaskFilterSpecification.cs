using TaskManagement.Application.Common.Specifications;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Enums;

namespace TaskManagement.Application.Features.Tasks.Specifications;

public class TaskFilterSpecification : BaseSpecification<TaskItem>
{
    public TaskFilterSpecification(TaskItemStatus? status, Priority? priority, int pageNumber, int pageSize)
    {
        if (status.HasValue || priority.HasValue)
        {
            AddCriteria(t =>
                (!status.HasValue || t.Status == status.Value) &&
                (!priority.HasValue || t.Priority == priority.Value));
        }

        ApplyPaging((pageNumber - 1) * pageSize, pageSize);
    }
}
