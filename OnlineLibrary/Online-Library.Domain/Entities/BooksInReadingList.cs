namespace Online_Library.Domain.Entities;

public class BooksInReadingList
{
    public Guid Id { get; set; }
    public Guid BookId { get; set; }
    public Book Book { get; set; }
    public Guid ReadingListId { get; set; }
    public ReadingList ReadingList { get; set; }
}