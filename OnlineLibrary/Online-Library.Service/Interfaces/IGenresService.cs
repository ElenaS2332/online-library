using Online_Library.Domain.Entities;

namespace Online_Library.Service.Interfaces;

public interface IGenresService
{
    Task<IEnumerable<Genre>> GetAllGenres();
    Task<Genre?> GetGenre(Guid id);
    Task InsertGenre(Genre genre);
    Task UpdateGenre(Genre genre);
    Task DeleteGenre(Genre genre);
}