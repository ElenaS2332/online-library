using Online_Library.Domain.Entities;

namespace Online_Library.Domain.Dtos;

public class ReadingListDto
{
    public int Count { get; set; }
    public List<BooksInReadingList> BooksInReadingList { get; set; }
}