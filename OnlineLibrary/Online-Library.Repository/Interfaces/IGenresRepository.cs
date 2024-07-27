using Online_Library.Domain.Entities;

namespace Online_Library.Repository.Interfaces;

public interface IGenresRepository
{
    IEnumerable<Genre> GetAllGenres();
    Genre GetGenre(Guid id);
    void InsertGenre(Genre genre);
    void UpdateGenre(Genre genre);
    void DeleteGenre(Genre genre);
}
