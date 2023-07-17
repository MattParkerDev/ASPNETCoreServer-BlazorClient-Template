using Domain.Entities;

namespace Application.Services;

public class TodoItemsService
{
    public TodoItem GetTodoItem(int id)
    {
        return new TodoItem
        {
            Id = id,
            Title = "Todo Item 1"
        };
    }
}