using MediatR;
using Microsoft.EntityFrameworkCore;
using TaskManagement.Application.Common.Models;
using TaskManagement.Application.Interfaces;

namespace TaskManagement.Application.Features.Tasks.Queries.GetAllTasks;

public class GetAllTasksQueryHandler : IRequestHandler<GetAllTasksQuery, List<TaskDto>>
{
    private readonly IApplicationDbContext _context;

    public GetAllTasksQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<TaskDto>> Handle(GetAllTasksQuery request, CancellationToken cancellationToken)
    {
        return await _context.Tasks
            .AsNoTracking()
            .Select(t => new TaskDto(
                t.Id,
                t.Title,
                t.Description,
                t.Status,
                t.Priority,
                t.DueDate,
                t.CreatedAt,
                t.UpdatedAt))
            .ToListAsync(cancellationToken);
    }
}
