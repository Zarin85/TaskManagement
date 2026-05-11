using MediatR;
using TaskManagement.Application.Interfaces;

namespace TaskManagement.Application.Features.Tasks.Commands.DeleteTask;

public class DeleteTaskCommandHandler : IRequestHandler<DeleteTaskCommand, bool>
{
    private readonly IApplicationDbContext _context;

    public DeleteTaskCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
    {
        var task = await _context.Tasks.FindAsync([request.Id], cancellationToken);

        if (task is null)
            return false;

        _context.Tasks.Remove(task);
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}
