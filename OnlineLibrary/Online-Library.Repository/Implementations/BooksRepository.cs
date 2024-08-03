using Microsoft.EntityFrameworkCore;
using Online_Library.Domain.Entities;
using Online_Library.Repository.Interfaces;

namespace Online_Library.Repository.Implementations;

public class BooksRepository(ApplicationDbContext context) : IBooksRepository
{
    public async Task<IEnumerable<Book>> GetAllBooks()
    {
        return await context.Books.ToListAsync();
    }

    public async Task<Book?> GetBook(Guid id)
    {
        return await context.Books.FirstOrDefaultAsync(b => b.Id == id);
    }

    public async Task InsertBook(Book book)
    {
        await context.Books.AddAsync(book);
        await context.SaveChangesAsync();
    }

    public async Task UpdateBook(Book book)
    {
        context.Books.Update(book);
        await context.SaveChangesAsync();
    }

    public async Task DeleteBook(Book book)
    {
        context.Books.Remove(book);
        await context.SaveChangesAsync();
    }
}
