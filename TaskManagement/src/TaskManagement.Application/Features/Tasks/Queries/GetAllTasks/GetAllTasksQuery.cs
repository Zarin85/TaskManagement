using MediatR;
using TaskManagement.Application.Common.Models;

namespace TaskManagement.Application.Features.Tasks.Queries.GetAllTasks;

public record GetAllTasksQuery : IRequest<List<TaskDto>>;
