using Online_Library.Domain.Entities;

namespace Online_Library.Service.Interfaces;

public interface IBooksService
{
    Task<IEnumerable<Book>> GetAllBooks();
    Task<Book?> GetBook(Guid id);
    Task InsertBook(Book book);
    Task UpdateBook(Book book);
    Task DeleteBook(Book book);
}