using Online_Library.Domain.Entities;

namespace Online_Library.Service.Interfaces;

public interface IGenresService
{
    IEnumerable<Genre> GetAllGenres();
    Genre GetGenre(Guid id);
    void InsertGenre(Genre genre);
    void UpdateGenre(Genre genre);
    void DeleteGenre(Genre genre);
}