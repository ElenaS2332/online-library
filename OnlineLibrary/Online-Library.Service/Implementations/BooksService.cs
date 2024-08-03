using Online_Library.Domain.Entities;
using Online_Library.Repository.Implementations;
using Online_Library.Repository.Interfaces;
using Online_Library.Service.Interfaces;

namespace Online_Library.Service.Implementations;

public class BooksService(IBooksRepository booksRepository) : IBooksService
{
    public async Task<IEnumerable<Book>> GetAllBooks()
    {
        return await booksRepository.GetAllBooks();
    }

    public async Task<Book?> GetBook(Guid id)
    {
        return await booksRepository.GetBook(id);
    }

    public async Task InsertBook(Book book)
    {
        await booksRepository.InsertBook(book);
    }

    public async Task UpdateBook(Book book)
    {
        await booksRepository.UpdateBook(book);
    }

    public async Task DeleteBook(Book book)
    {
        await booksRepository.DeleteBook(book);
    }
}