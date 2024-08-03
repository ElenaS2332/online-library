using Online_Library.Domain.Entities;

namespace Online_Library.Repository.Interfaces;

public interface IBooksRepository
{
    Task<IEnumerable<Book>> GetAllBooksAsync();
    Task<Book?> GetBookAsync(Guid id);
    Task<Book> InsertBookAsync(Book book);
    Task<Book> UpdateBookAsync(Book book);
    Task DeleteBookAsync(Book book);
}