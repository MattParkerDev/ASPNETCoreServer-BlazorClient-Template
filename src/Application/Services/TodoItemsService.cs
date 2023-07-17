using Application.Validation;
using Domain.Entities;
using FluentValidation.Results;
using OneOf;
using OneOf.Types;

namespace Application.Services;

public class TodoItemsService
{
    private static readonly List<TodoItem> TodoItemsList = new List<TodoItem>
    {
        new TodoItem
        {
            Id = 1,
            Title = "Todo Item 1"
        },
        new TodoItem
        {
            Id = 2,
            Title = "Todo Item 2"
        }
    };

    public OneOf<TodoItem, NotFound> GetTodoItem(int id)
    {
        var todoItem = TodoItemsList.FirstOrDefault(s => s.Id == id);
        if (todoItem is null)
        {
            return new NotFound();
        }

        return todoItem;
    }
    
    public OneOf<TodoItem, ValidationFailed> CreateTodoItem(int id, string title)
    {
        var todoItem = new TodoItem
        {
            Id = id,
            Title = title
        };
        
        var validationResult = ValidateTodoItem(todoItem);
        if (validationResult.IsValid is false)
        {
            return new ValidationFailed(validationResult.Errors);
        }

        TodoItemsList.Add(todoItem);
        return todoItem;
    }

    private ValidationResult ValidateTodoItem(TodoItem todoItem)
    {
        // if todoItem.Id is in TodoItemsList
        if (TodoItemsList.Any(s => s.Id == todoItem.Id))
        {
            return new ValidationResult(new List<ValidationFailure>
            {
                new ValidationFailure("Id", "Id already exists")
            });
        }

        return new ValidationResult();
    }
}