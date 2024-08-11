namespace OnlineLibraryAdmin.Models;

public class Author
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public DateTime DateOfBirth { get; set; }
}