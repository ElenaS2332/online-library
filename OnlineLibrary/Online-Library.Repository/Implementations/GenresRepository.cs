using Microsoft.EntityFrameworkCore;
using Online_Library.Domain.Entities;
using Online_Library.Repository.Interfaces;

namespace Online_Library.Repository.Implementations;

public class GenresRepository(ApplicationDbContext context) : IGenresRepository
{
    public async Task<IEnumerable<Genre>> GetAllGenres()
    {
        return await context.Genres.ToListAsync();
    }

    public async Task<Genre> GetGenre(Guid id)
    {
        return await context.Genres.FirstOrDefaultAsync(g => g.Id == id);
    }

    public async Task InsertGenre(Genre genre)
    {
        await context.Genres.AddAsync(genre);
        await context.SaveChangesAsync();
    }

    public async Task UpdateGenre(Genre genre)
    {
        context.Genres.Update(genre);
        await context.SaveChangesAsync();
    }

    public async Task DeleteGenre(Genre genre)
    {
        context.Genres.Remove(genre);
        await context.SaveChangesAsync();
    }
}
