namespace Online_Library.Domain.Entities;

public class Genre
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public virtual ICollection<Book>? Books { get; set; }
}
