using MediatR;
using TaskManagement.API.Requests;
using TaskManagement.Application.Features.Tasks.Commands.CreateTask;
using TaskManagement.Application.Features.Tasks.Commands.DeleteTask;
using TaskManagement.Application.Features.Tasks.Commands.UpdateTask;
using TaskManagement.Application.Features.Tasks.Queries.GetAllTasks;
using TaskManagement.Application.Features.Tasks.Queries.GetTaskById;
using TaskManagement.Domain.Enums;

namespace TaskManagement.API.Endpoints;

public static class TaskEndpoints
{
    public static void MapTaskEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/tasks")
            .WithTags("Tasks");

        group.MapGet("/", GetAllTasks);

        group.MapGet("/{id:guid}", GetTaskById)
            .WithName("GetTaskById");

        group.MapPost("/", CreateTask)
            .Produces(StatusCodes.Status201Created);

        group.MapPut("/{id:guid}", UpdateTask)
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound);

        group.MapDelete("/{id:guid}", DeleteTask)
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound);
    }

    private static async Task<IResult> GetAllTasks(
        ISender sender,
        CancellationToken ct,
        TaskItemStatus? status = null,
        Priority? priority = null,
        int pageNumber = 1,
        int pageSize = 10)
    {
        var result = await sender.Send(new GetAllTasksQuery(status, priority, pageNumber, pageSize), ct);
        return Results.Ok(result);
    }

    private static async Task<IResult> GetTaskById(Guid id, ISender sender, CancellationToken ct)
    {
        var dto = await sender.Send(new GetTaskByIdQuery(id), ct);
        return Results.Ok(dto);
    }

    private static async Task<IResult> CreateTask(
        CreateTaskRequest request,
        ISender sender,
        CancellationToken ct)
    {
        var command = new CreateTaskCommand(
            request.Title,
            request.Description,
            request.Priority,
            request.DueDate);

        var id = await sender.Send(command, ct);
        return Results.CreatedAtRoute("GetTaskById", new { id }, new { id });
    }

    private static async Task<IResult> UpdateTask(
        Guid id,
        UpdateTaskRequest request,
        ISender sender,
        CancellationToken ct)
    {
        var command = new UpdateTaskCommand(
            id,
            request.Title,
            request.Description,
            request.Priority,
            request.Status,
            request.DueDate);

        await sender.Send(command, ct);
        return Results.NoContent();
    }

    private static async Task<IResult> DeleteTask(Guid id, ISender sender, CancellationToken ct)
    {
        await sender.Send(new DeleteTaskCommand(id), ct);
        return Results.NoContent();
    }
}
