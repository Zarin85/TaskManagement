using MediatR;
using TaskManagement.Application.Common.Models;

namespace TaskManagement.Application.Features.Tasks.Queries.GetTaskById;

public record GetTaskByIdQuery(Guid Id) : IRequest<TaskDto>;
