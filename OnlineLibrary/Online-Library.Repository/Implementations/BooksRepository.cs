using Online_Library.Domain.Entities;
using Online_Library.Repository.Interfaces;

namespace Online_Library.Repository.Implementations;

public class BooksRepository(ApplicationDbContext context) : IBooksRepository
{
    public IEnumerable<Book> GetAllBooks()
    {
        return context.Books.ToList();
    }

    public Book GetBook(Guid id)
    {
        return context.Books.FirstOrDefault(b => b.Id == id);
    }

    public void InsertBook(Book book)
    {
        context.Books.Add(book);
        context.SaveChanges();
    }

    public void UpdateBook(Book book)
    {
        context.Books.Update(book);
        context.SaveChanges();
    }

    public void DeleteBook(Book book)
    {
        context.Books.Remove(book);
        context.SaveChanges();
    }
}
