using Microsoft.EntityFrameworkCore;
using Online_Library.Domain.Entities;
using Online_Library.Repository.Interfaces;

namespace Online_Library.Repository.Implementations;

public class GenresRepository(ApplicationDbContext context) : IGenresRepository
{
    public IEnumerable<Genre> GetAllBGenres()
    {
        return context.Genres.ToList();
    }

    public Genre? GetGenre(Guid id)
    {
        return context.Genres.FirstOrDefault(g => g.Id == id);
    }

    public Genre? GetGenreByName(string name)
    {
        return context.Genres.FirstOrDefault(g => g.Name == name);
    }

    public async Task<IEnumerable<Genre>> GetAllGenresAsync()
    {
        return await context.Genres.ToListAsync();
    }

    public async Task<Genre?> GetGenreAsync(Guid id)
    {
        return await context.Genres.FirstOrDefaultAsync(g => g.Id == id);
    }

    public async Task InsertGenreAsync(Genre genre)
    {
        await context.Genres.AddAsync(genre);
        await context.SaveChangesAsync();
    }

    public async Task UpdateGenreAsync(Genre genre)
    {
        await context.SaveChangesAsync();
    }

    public async Task DeleteGenreAsync(Genre genre)
    {
        context.Genres.Remove(genre);
        await context.SaveChangesAsync();
    }
}
