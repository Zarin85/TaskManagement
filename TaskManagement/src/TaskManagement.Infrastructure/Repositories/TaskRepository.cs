using Microsoft.EntityFrameworkCore;
using TaskManagement.Application.Common.Specifications;
using TaskManagement.Application.Interfaces;
using TaskManagement.Domain.Entities;
using TaskManagement.Infrastructure.Persistence;

namespace TaskManagement.Infrastructure.Repositories;

public class TaskRepository : ITaskRepository
{
    private readonly AppDbContext _context;

    public TaskRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<TaskItem?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => await _context.Tasks.FindAsync([id], cancellationToken);

    public async Task<IReadOnlyList<TaskItem>> GetAllAsync(ISpecification<TaskItem> spec, CancellationToken cancellationToken = default)
        => await SpecificationEvaluator<TaskItem>
            .GetQuery(_context.Tasks.AsNoTracking(), spec)
            .ToListAsync(cancellationToken);

    public async Task<int> CountAsync(ISpecification<TaskItem> spec, CancellationToken cancellationToken = default)
    {
        var query = _context.Tasks.AsNoTracking().AsQueryable();

        if (spec.Criteria != null)
            query = query.Where(spec.Criteria);

        return await query.CountAsync(cancellationToken);
    }

    public async Task AddAsync(TaskItem task, CancellationToken cancellationToken = default)
    {
        await _context.Tasks.AddAsync(task, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(TaskItem task, CancellationToken cancellationToken = default)
    {
        _context.Tasks.Update(task);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(TaskItem task, CancellationToken cancellationToken = default)
    {
        _context.Tasks.Remove(task);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
