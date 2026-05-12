using MediatR;
using TaskManagement.Application.Common.Models;
using TaskManagement.Application.Features.Tasks.Specifications;
using TaskManagement.Application.Interfaces;

namespace TaskManagement.Application.Features.Tasks.Queries.GetAllTasks;

public class GetAllTasksQueryHandler : IRequestHandler<GetAllTasksQuery, PaginatedResult<TaskDto>>
{
    private readonly ITaskRepository _repository;

    public GetAllTasksQueryHandler(ITaskRepository repository)
    {
        _repository = repository;
    }

    public async Task<PaginatedResult<TaskDto>> Handle(GetAllTasksQuery request, CancellationToken cancellationToken)
    {
        var spec = new TaskFilterSpecification(request.Status, request.Priority, request.PageNumber, request.PageSize);

        var tasks = await _repository.GetAllAsync(spec, cancellationToken);
        var totalCount = await _repository.CountAsync(spec, cancellationToken);

        var dtos = tasks
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

        return new PaginatedResult<TaskDto>(dtos, totalCount, request.PageNumber, request.PageSize);
    }
}
