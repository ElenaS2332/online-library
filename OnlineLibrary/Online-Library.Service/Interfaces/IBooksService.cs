using Online_Library.Domain.Dtos;
using Online_Library.Domain.Entities;

namespace Online_Library.Service.Interfaces;

public interface IBooksService
{
    Task<IEnumerable<Book>> GetAllBooksAsync();
    Task<Book?> GetBookAsync(Guid id);
    Task<Book> InsertBookAsync(BookDto bookDto);
    Task<Book> UpdateBookAsync(EditBookDto bookDto);
    Task DeleteBookAsync(Book book);
}