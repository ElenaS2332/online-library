using Online_Library.Domain.Entities;

namespace Online_Library.Repository.Interfaces;

public interface IBooksRepository
{
    IEnumerable<Book> GetAllBooks();
    Book GetBook(Guid id);
    void InsertBook(Book book);
    void UpdateBook(Book book);
    void DeleteBook(Book book);
}