using Online_Library.Domain.Entities;

namespace Online_Library.Repository.Interfaces;

public interface IGenresRepository
{
    IEnumerable<Genre> GetAllBGenres();
    Genre? GetGenre(Guid id);

    Genre? GetGenreByName(string name);
    Task<IEnumerable<Genre>> GetAllGenresAsync();
    Task<Genre?> GetGenreAsync(Guid id);
    Task InsertGenreAsync(Genre genre);
    Task UpdateGenreAsync(Genre genre);
    Task DeleteGenreAsync(Genre genre);
}
