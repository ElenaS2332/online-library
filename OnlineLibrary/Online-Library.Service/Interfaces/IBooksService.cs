using Online_Library.Domain.Entities;

namespace Online_Library.Service.Interfaces;

public interface IBooksService
{
    IEnumerable<Book> GetAllBooks();
    Book GetBook(Guid id);
    void InsertBook(Book book);
    void UpdateBook(Book book);
    void DeleteBook(Book book);
}