using Microsoft.EntityFrameworkCore;
using Online_Library.Domain.Entities;
using Online_Library.Repository.Interfaces;

namespace Online_Library.Repository.Implementations;

public class AuthorsRepository(ApplicationDbContext context) : IAuthorsRepository
{
    public async Task<IEnumerable<Author>> GetAllAuthors()
    {
        return await context.Authors.ToListAsync();
    }

    public async Task<Author> GetAuthor(Guid id)
    {
        return await context.Authors.FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task InsertAuthor(Author author)
    {
        await context.Authors.AddAsync(author);
        await context.SaveChangesAsync();
    }

    public async Task UpdateAuthor(Author author)
    {
        await context.SaveChangesAsync();
    }

    public async Task DeleteAuthor(Author author)
    {
        context.Authors.Remove(author);
        await context.SaveChangesAsync();    
    }
}
