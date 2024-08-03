namespace Online_Library.Domain.Dtos;

public class BookDto
{
    public Guid Id { get; set; }
    public string? Title { get; set; }
    public DateTime PublishDate { get; set; }
    public string? ISBN { get; set; }
    public string? Description { get; set; }
    public Guid AuthorId { get; set; }
    public Guid GenreId { get; set; }
}