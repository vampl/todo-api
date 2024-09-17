using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using TodoApi.Data;
using TodoApi.Models;

namespace TodoApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TodoController : ControllerBase
{
    private readonly TodoApiDbContext _context;

    public TodoController(TodoApiDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TodoItem>>> GetTodoItems()
    {
        return await _context.TodoItems.ToListAsync();
    }

    [HttpGet("{id:long}")]
    public async Task<ActionResult<TodoItem>> GetTodoItem(long id)
    {
        TodoItem? product = await _context.TodoItems.FindAsync(id);

        if (product == null)
        {
            return NotFound();
        }

        return product;
    }

    [HttpPost]
    public async Task<ActionResult<TodoItem>> PostTodoItem(TodoItem todoItem)
    {
        await _context.TodoItems.AddAsync(todoItem);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(PostTodoItem), new { id = todoItem.Id }, todoItem);
    }


    [HttpPut("{id:long}")]
    public async Task<ActionResult> PutTodoItem(long id, TodoItem todoItem)
    {
        TodoItem? existingTodoItem = await _context.TodoItems.FindAsync(id);

        if (existingTodoItem == null)
        {
            return BadRequest();
        }

        _context.Entry(existingTodoItem).CurrentValues.SetValues(todoItem);
        _context.Entry(existingTodoItem).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return NoContent();
    }


    [HttpDelete("{id:long}")]
    public async Task<IActionResult> DeleteTodoItem(long id)
    {
        TodoItem? todoItem = await _context.TodoItems.FindAsync(id);

        if (todoItem == null)
        {
            return NotFound();
        }

        _context.TodoItems.Remove(todoItem);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
