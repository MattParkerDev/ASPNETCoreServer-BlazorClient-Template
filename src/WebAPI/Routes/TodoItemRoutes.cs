using Application.Services;
using Application.Validation;
using Domain.Entities;
using Microsoft.AspNetCore.Http.HttpResults;

namespace WebAPI.Routes;

public static class TodoItemRoutes
{
    public static void MapTodoItemRoutes(this WebApplication app)
    {
        var group = app.MapGroup("").WithTags("TodoItems").AllowAnonymous().WithOpenApi();

        group
            .MapGet(
                "/todoitems/{id}",
                Results<Ok<TodoItem>, NotFound> (TodoItemsService todoItemsService, HttpContext context, string id) =>
                {
                    var result = todoItemsService.GetTodoItem(int.Parse(id));
                    
                    return result.Match<Results<Ok<TodoItem>, NotFound>>(
                        todoItem => TypedResults.Ok(todoItem),
                        _ => TypedResults.NotFound());
                }
            )
            .WithName("TodoItems");
        
        group
            .MapPost(
                "/todoitems",
                Results<CreatedAtRoute<TodoItem>, BadRequest<ValidationFailed>> (TodoItemsService todoItemsService, HttpContext context, int id, string title) =>
                {
                    var result = todoItemsService.CreateTodoItem(id, title);
                    
                    return result.Match<Results<CreatedAtRoute<TodoItem>, BadRequest<ValidationFailed>>>(
                        todoItem => TypedResults.CreatedAtRoute(todoItem, "TodoItems", new { id = todoItem.Id}),
                        validationFailed => TypedResults.BadRequest(validationFailed));
                }
            )
            .WithName("Create TodoItem");
    }
}
