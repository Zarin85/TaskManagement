using MediatR;
using TaskManagement.Application.Common.Models;
using TaskManagement.Domain.Enums;

namespace TaskManagement.Application.Features.Tasks.Queries.GetAllTasks;

public record GetAllTasksQuery(
    TaskItemStatus? Status = null,
    Priority? Priority = null,
    int PageNumber = 1,
    int PageSize = 10
) : IRequest<PaginatedResult<TaskDto>>;
