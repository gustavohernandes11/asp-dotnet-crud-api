using Microsoft.AspNetCore.Mvc;
using MeuTodo.Data;
using Microsoft.EntityFrameworkCore;
using CrudApp.Models;
using CrudApp.ViewModels;

[ApiController]
[Route(template: "v1")]
public class TodoController : ControllerBase
{

    [HttpGet]
    [Route(template: "todos")]
    public async Task<IActionResult> GetAsync(
            [FromServices] AppDataContext context)
    {
        var todos = await context.Todos.AsNoTracking().Select(x => x).ToListAsync();

        return todos == null
        ? NotFound()
        : Ok(todos);
    }

    [HttpGet]
    [Route(template: "todos/{id}")]
    public async Task<IActionResult> GetAsync(
        [FromServices] AppDataContext context,
        [FromRoute] int id)
    {
        var todo = await context.Todos.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

        return todo == null
        ? NotFound()
        : Ok(todo);
    }

    [HttpPost]
    [Route(template: "todos")]
    public async Task<IActionResult> PostAsync(
        [FromServices] AppDataContext context,
        [FromBody] CreateTodoViewModel model
        )
    {
        if (!ModelState.IsValid)
            return BadRequest();

        var todo = new Todo
        {
            CreatedAt = DateTime.Now,
            Title = model.Title,
            Done = false,
        };

        try
        {
            await context.Todos.AddAsync(todo);
            await context.SaveChangesAsync();
            return Created($"v1/todos/{todo.Id}", todo);
        }
        catch
        {
            return BadRequest();
        }
    }

    [HttpPut]
    [Route(template: "todos/{Id}")]
    public async Task<IActionResult> PutAsync(
        [FromServices] AppDataContext context,
        [FromBody] CreateTodoViewModel model,
        [FromRoute] int Id)
    {
        if (!ModelState.IsValid)
            return BadRequest();

        var todo = await context.Todos.FirstOrDefaultAsync(x => x.Id == Id);

        if (todo == null)
            return NotFound();

        try
        {
            todo.Title = model.Title;
            todo.Done = model.Done;

            context.Todos.Update(todo);
            context.SaveChanges();
            return Ok(todo);
        }
        catch
        {
            return BadRequest();
        }
    }

    [HttpDelete]
    [Route(template: "todos/{Id}")]
    public async Task<IActionResult> DeleteAsync(
        [FromServices] AppDataContext context,
        [FromRoute] int Id)
    {

        var todo = await context.Todos.FirstOrDefaultAsync(x => x.Id == Id);

        if (todo == null)
            return NotFound();

        try
        {
            context.Todos.Remove(todo);
            context.SaveChanges();
            return Ok(todo);
        }
        catch
        {
            return BadRequest();
        }
    }

}
