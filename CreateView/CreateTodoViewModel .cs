using System.ComponentModel.DataAnnotations;

namespace CrudApp.ViewModels;

public class CreateTodoViewModel
{
    [Required]
    public required string Title { get; set; }
    [Required]
    public bool Done { get; set; }
}
