using System.ComponentModel.DataAnnotations;
using NotesApp.Models;

public class Category
{
    public Guid Id { get; set; }

    [Required(ErrorMessage = "Название категории обязательно")]
    public string Name { get; set; } = string.Empty;

    public List<Note> Notes { get; set; } = new();
}