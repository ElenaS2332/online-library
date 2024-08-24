namespace Online_Library.Domain.Entities;

public class Book
{
    public Guid Id { get; set;}
    public string? Title { get; set; }
    public DateTime PublishDate { get; set; }
    public string? ISBN { get; set; }
    public string? Description { get; set; }
    
    public Guid? AuthorId { get; set; }
    public virtual Author? Author { get; set; }
    public Guid? GenreId { get; set; }
    public virtual Genre? Genre { get; set; }

    public virtual ICollection<BooksInReadingList>? BookInReadingLists { get; set; }
}