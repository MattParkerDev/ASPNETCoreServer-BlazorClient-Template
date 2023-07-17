using Application.Services;
using Microsoft.AspNetCore.Http.HttpResults;

namespace WebAPI.Routes;

public static class TodoItemRoutes
{
    public static void MapTodoItemRoutes(this WebApplication app)
    {
        var group = app.MapGroup("").WithTags("TodoItems").RequireAuthorization().WithOpenApi();

        group
            .MapGet(
                "/todoitems",
                Ok<string> (TodoItemsService todoItemsService, HttpContext context) =>
                {
                    return TypedResults.Ok(todoItemsService.GetTodoItem(1));
                }
            )
            .WithName("TodoItems");
    }
}
