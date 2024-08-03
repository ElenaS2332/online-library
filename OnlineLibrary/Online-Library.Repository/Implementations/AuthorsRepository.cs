using Microsoft.EntityFrameworkCore;
using Online_Library.Domain.Entities;
using Online_Library.Repository.Interfaces;

namespace Online_Library.Repository.Implementations;

public class AuthorsRepository(ApplicationDbContext context) : IAuthorsRepository
{
    public IEnumerable<Author> GetAllAuthors()
    {
        return context.Authors.ToList();
    }

    public Author? GetAuthor(Guid id)
    {
        return context.Authors.FirstOrDefault(a => a.Id == id);
    }

    public async Task<IEnumerable<Author>> GetAllAuthorsAsync()
    {
        return await context.Authors.ToListAsync();
    }

    public async Task<Author?> GetAuthorAsync(Guid id)
    {
        return await context.Authors.FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task InsertAuthorAsync(Author author)
    {
        await context.Authors.AddAsync(author);
        await context.SaveChangesAsync();
    }

    public async Task UpdateAuthorAsync(Author author)
    {
        await context.SaveChangesAsync();
    }

    public async Task DeleteAuthorAsync(Author author)
    {
        context.Authors.Remove(author);
        await context.SaveChangesAsync();    
    }
}
