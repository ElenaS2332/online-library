using Online_Library.Domain.Entities;

namespace Online_Library.Service.Interfaces;

public interface IGenresService
{
    IEnumerable<Genre> GetAllGenres();
    Genre? GetGenre(Guid id);
    Task<IEnumerable<Genre>> GetAllGenresAsync();
    Task<Genre?> GetGenreAsync(Guid id);
    Task InsertGenreAsync(Genre genre);
    Task UpdateGenreAsync(Genre genre);
    Task DeleteGenreAsync(Genre genre);
}