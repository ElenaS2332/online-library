using Online_Library.Domain.Entities;

namespace Online_Library.Repository.Interfaces;

public interface IBooksInReadingListRepository
{
    IEnumerable<BooksInReadingList> GetAllBooksInReadingList();
    BooksInReadingList? GetBooksInReadingList(Guid id);
    void InsertBooksInReadingList(BooksInReadingList booksInReadingList);
    void UpdateBooksInReadingList(BooksInReadingList booksInReadingList);
    void DeleteBooksInReadingList(BooksInReadingList booksInReadingList);
}