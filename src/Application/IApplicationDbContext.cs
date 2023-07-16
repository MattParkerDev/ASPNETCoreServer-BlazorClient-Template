using Domain;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application;

public interface IApplicationDbContext
{
    DbSet<TodoItem> TodoItems { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}