namespace Online_Library.Domain.Entities;

public class ReadingList
{
    public Guid Id { get; set; }
    public string? UserId { get; set; }
    public User? User { get; set; }
    public virtual ICollection<BooksInReadingList>? BooksInReadingList { get; set; }
}