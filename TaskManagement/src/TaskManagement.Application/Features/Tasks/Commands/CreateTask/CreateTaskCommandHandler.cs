using MediatR;
using TaskManagement.Application.Interfaces;
using TaskManagement.Domain.Entities;

namespace TaskManagement.Application.Features.Tasks.Commands.CreateTask;

public class CreateTaskCommandHandler : IRequestHandler<CreateTaskCommand, Guid>
{
    private readonly ITaskRepository _repository;

    public CreateTaskCommandHandler(ITaskRepository repository)
    {
        _repository = repository;
    }

    public async Task<Guid> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
    {
        var task = TaskItem.Create(request.Title, request.Description, request.Priority, request.DueDate);

        await _repository.AddAsync(task, cancellationToken);

        return task.Id;
    }
}
