using Microsoft.EntityFrameworkCore;
using Online_Library.Domain.Entities;
using Online_Library.Repository.Interfaces;

namespace Online_Library.Repository.Implementations;

public class BooksRepository(ApplicationDbContext context) : IBooksRepository
{
    public async Task<IEnumerable<Book>> GetAllBooksAsync()
    {
        return await context.Books
            .Include(b => b.Author)
            .Include(b => b.Genre)
            .ToListAsync();
    }

    public async Task<Book?> GetBookAsync(Guid id)
    {
        return await context.Books
            .Include(b => b.Author)
            .Include(b => b.Genre)
            .FirstOrDefaultAsync(b => b.Id == id);
    }

    public async Task<Book> InsertBookAsync(Book book)
    {
        await context.Books.AddAsync(book);
        await context.SaveChangesAsync();
        return book;
    }

    public async Task<Book> UpdateBookAsync(Book book)
    {
        context.Books.Update(book);
        await context.SaveChangesAsync();
        return book;
    }

    public async Task DeleteBookAsync(Book book)
    {
        context.Books.Remove(book);
        await context.SaveChangesAsync();
    }
}
