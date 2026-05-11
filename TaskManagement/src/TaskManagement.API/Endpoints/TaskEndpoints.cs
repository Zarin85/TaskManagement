using MediatR;
using TaskManagement.API.Requests;
using TaskManagement.Application.Features.Tasks.Commands.CreateTask;
using TaskManagement.Application.Features.Tasks.Commands.DeleteTask;
using TaskManagement.Application.Features.Tasks.Commands.UpdateTask;
using TaskManagement.Application.Features.Tasks.Queries.GetAllTasks;
using TaskManagement.Application.Features.Tasks.Queries.GetTaskById;

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

    private static async Task<IResult> GetAllTasks(ISender sender, CancellationToken ct)
    {
        var tasks = await sender.Send(new GetAllTasksQuery(), ct);
        return Results.Ok(tasks);
    }

    private static async Task<IResult> GetTaskById(Guid id, ISender sender, CancellationToken ct)
    {
        var result = await sender.Send(new GetTaskByIdQuery(id), ct);
        return result.IsSuccess
            ? Results.Ok(result.Value)
            : Results.NotFound(result.Error);
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

        var success = await sender.Send(command, ct);
        return success ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> DeleteTask(Guid id, ISender sender, CancellationToken ct)
    {
        var success = await sender.Send(new DeleteTaskCommand(id), ct);
        return success ? Results.NoContent() : Results.NotFound();
    }
}
