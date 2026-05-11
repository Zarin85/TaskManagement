using MediatR;
using TaskManagement.Application.Common.Models;
using TaskManagement.Application.Interfaces;

namespace TaskManagement.Application.Features.Tasks.Queries.GetTaskById;

public class GetTaskByIdQueryHandler : IRequestHandler<GetTaskByIdQuery, Result<TaskDto>>
{
    private readonly ITaskRepository _repository;

    public GetTaskByIdQueryHandler(ITaskRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<TaskDto>> Handle(GetTaskByIdQuery request, CancellationToken cancellationToken)
    {
        var task = await _repository.GetByIdAsync(request.Id, cancellationToken);

        if (task is null)
            return Result<TaskDto>.Failure($"Task with id '{request.Id}' was not found.");

        var dto = new TaskDto(
            task.Id,
            task.Title,
            task.Description,
            task.Status,
            task.Priority,
            task.DueDate,
            task.CreatedAt,
            task.UpdatedAt);

        return Result<TaskDto>.Success(dto);
    }
}
