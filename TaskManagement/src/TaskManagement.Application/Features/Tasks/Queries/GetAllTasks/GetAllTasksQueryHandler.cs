using MediatR;
using TaskManagement.Application.Common.Models;
using TaskManagement.Application.Interfaces;

namespace TaskManagement.Application.Features.Tasks.Queries.GetAllTasks;

public class GetAllTasksQueryHandler : IRequestHandler<GetAllTasksQuery, List<TaskDto>>
{
    private readonly ITaskRepository _repository;

    public GetAllTasksQueryHandler(ITaskRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<TaskDto>> Handle(GetAllTasksQuery request, CancellationToken cancellationToken)
    {
        var tasks = await _repository.GetAllAsync(cancellationToken);

        return tasks
            .Select(t => new TaskDto(
                t.Id,
                t.Title,
                t.Description,
                t.Status,
                t.Priority,
                t.DueDate,
                t.CreatedAt,
                t.UpdatedAt))
            .ToList();
    }
}
