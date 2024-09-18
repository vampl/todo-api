using Microsoft.EntityFrameworkCore;

using TodoApi.Models;

namespace TodoApi.Data;

public class TodoApiDbContext : DbContext
{
    public TodoApiDbContext(DbContextOptions<TodoApiDbContext> options) : base(options)
    {
    }

    public DbSet<TodoItem> TodoItems { get; set; }
}
