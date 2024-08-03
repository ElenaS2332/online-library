using Online_Library.Domain.Entities;

namespace Online_Library.Repository.Interfaces;

public interface IBooksRepository
{
    Task<IEnumerable<Book>> GetAllBooks();
    Task<Book?> GetBook(Guid id);
    Task InsertBook(Book book);
    Task UpdateBook(Book book);
    Task DeleteBook(Book book);
}