using Online_Library.Domain.Entities;

namespace Online_Library.Domain.Dtos;

public class EditBookDto
{
    public Guid Id { get; set; }
    public string? Title { get; set; }
    public DateTime PublishDate { get; set; }
    public string? ISBN { get; set; }
    public string? Description { get; set; }
    public Guid? AuthorId { get; set; }
    public Guid? GenreId { get; set; }
    public Author? Author { get; set; }
    public Genre? Genre { get; set; }

}