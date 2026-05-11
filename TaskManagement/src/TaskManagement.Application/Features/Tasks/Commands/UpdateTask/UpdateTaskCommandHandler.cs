using MediatR;
using TaskManagement.Application.Interfaces;

namespace TaskManagement.Application.Features.Tasks.Commands.UpdateTask;

public class UpdateTaskCommandHandler : IRequestHandler<UpdateTaskCommand, bool>
{
    private readonly IApplicationDbContext _context;

    public UpdateTaskCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
    {
        var task = await _context.Tasks.FindAsync([request.Id], cancellationToken);

        if (task is null)
            return false;

        task.Update(request.Title, request.Description, request.Priority, request.DueDate);
        task.ChangeStatus(request.Status);

        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}
