using Online_Library.Domain.Entities;
using Online_Library.Repository.Implementations;
using Online_Library.Repository.Interfaces;
using Online_Library.Service.Interfaces;

namespace Online_Library.Service.Implementations;

public class BooksService(IBooksRepository booksRepository) : IBooksService
{
    public IEnumerable<Book> GetAllBooks()
    {
        return booksRepository.GetAllBooks();
    }

    public Book GetBook(Guid id)
    {
        return booksRepository.GetBook(id);
    }

    public void InsertBook(Book book)
    {
        booksRepository.InsertBook(book);
    }

    public void UpdateBook(Book book)
    {
        booksRepository.UpdateBook(book);
    }

    public void DeleteBook(Book book)
    {
        booksRepository.DeleteBook(book);
    }
}