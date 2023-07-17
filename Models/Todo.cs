namespace CrudApp.Models;

public class Todo
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public bool Done { get; set; }
    public DateTime CreatedAt { get; set; }
}
