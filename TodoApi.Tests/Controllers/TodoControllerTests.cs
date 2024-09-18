using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Moq;

using TodoApi.Controllers;
using TodoApi.Data;
using TodoApi.Models;

namespace TodoApi.Tests.Controllers;

public class TodoControllerTests
{
    private readonly TodoController _controller;
    private readonly TodoApiDbContext _context;

    public TodoControllerTests()
    {
        // Configure the in-memory database
        DbContextOptions<TodoApiDbContext> options = new DbContextOptionsBuilder<TodoApiDbContext>()
            .UseInMemoryDatabase(databaseName: $"TodoDB{Guid.NewGuid()}")
            .Options;

        _context = new TodoApiDbContext(options);
        _controller = new TodoController(_context);

        // Seed the in-memory database with some data
        SeedDatabase();
    }

    private void SeedDatabase()
    {
        _context.TodoItems.AddRange(
            new TodoItem { Id = 1, Title = "Test 1", Description = "Text", IsCompleted = true },
            new TodoItem { Id = 2, Title = "Test 2", Description = "Text", IsCompleted = false }
        );
        _context.SaveChanges();
    }

    [Fact]
    public async Task GetTodoItems_NotEmptyContext_ShouldReturnAllItems()
    {
        // Arrange
        List<TodoItem> items = new()
        {
            new TodoItem { Id = 1, Title = "Test 1", Description = "Text", IsCompleted = true },
            new TodoItem { Id = 2, Title = "Test 2", Description = "Text", IsCompleted = false },
        };

        // Act
        ActionResult<IEnumerable<TodoItem>> result = await _controller.GetTodoItems();

        // Assert
        var okResult = Assert.IsType<ActionResult<IEnumerable<TodoItem>>>(result);
        var returnValue = Assert.IsType<List<TodoItem>>(okResult.Value);
        Assert.Equal(2, returnValue.Count);
    }

    [Fact]
    public async Task GetTodoItem_ValidInputAndNotEmptyContext_ShouldReturnItem()
    {
        // Arrange
        var item = new TodoItem
            { Id = 1, Title = "Test 1", Description = "Text", IsCompleted = true };

        // Act
        ActionResult<TodoItem> result = await _controller.GetTodoItem(1);

        // Assert
        var okResult = Assert.IsType<ActionResult<TodoItem>>(result);
        Assert.Equal(item.Id, okResult.Value.Id);
    }

    [Fact]
    public async Task GetTodoItem_ValidInputAndEmptyContext_ShouldReturnNotFound()
    {
        // Arrange

        // Act
        ActionResult<TodoItem> result = await _controller.GetTodoItem(3);

        // Assert
        Assert.IsType<NotFoundResult>(result.Result);
    }

    [Fact]
    public async Task PostTodoItem_ValidData_ShouldReturnCreatedItem()
    {
        // Arrange
        TodoItem item = new() { Id = 0, Title = "Test 1", Description = "Text", IsCompleted = true };

        // Act
        ActionResult<TodoItem> result = await _controller.PostTodoItem(item);

        // Assert
        var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
        var returnValue = Assert.IsType<TodoItem>(createdAtActionResult.Value);
        Assert.Equal(item.Id, returnValue.Id);
    }

    [Fact]
    public async Task PutTodoItem_ValidDataNotEmptyContext_ShouldUpdatesExistingItem()
    {
        // Arrange
        TodoItem existingItem = new() { Id = 1, Title = "Test 1", Description = "Text", IsCompleted = true };

        TodoItem updatedItem = new() { Id = 1, Title = "Updated Title 1", Description = "Text", IsCompleted = true };

        // Act
        ActionResult result = await _controller.PutTodoItem(1, updatedItem);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task PutTodoItem_EmptyContext_ShouldReturnBadRequest()
    {
        // Arrange
        TodoItem updatedItem = new() { Id = 1, Title = "Updated Title 1", Description = "Text", IsCompleted = true };

        // Act
        ActionResult result = await _controller.PutTodoItem(3, updatedItem);

        // Assert
        Assert.IsType<BadRequestResult>(result);
    }

    [Fact]
    public async Task DeleteTodoItem_NotEmptyContext_ShouldReturnNoContent()
    {
        // Arrange

        // Act
        IActionResult result = await _controller.DeleteTodoItem(1);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task DeleteTodoItem_EmptyContext_ShouldReturnNotFound()
    {
        // Arrange

        // Act
        IActionResult result = await _controller.DeleteTodoItem(3);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }
}
