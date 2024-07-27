using Online_Library.Domain.Entities;
using Online_Library.Repository.Interfaces;
using Online_Library.Service.Interfaces;

namespace Online_Library.Service.Implementations;

public class GenresService(IGenresRepository genresRepository) : IGenresService
{
    public IEnumerable<Genre> GetAllGenres()
    {
        return genresRepository.GetAllGenres();
    }

    public Genre GetGenre(Guid id)
    {
        return genresRepository.GetGenre(id);
    }

    public void InsertGenre(Genre genre)
    {
        genresRepository.InsertGenre(genre);
    }

    public void UpdateGenre(Genre genre)
    {
        genresRepository.UpdateGenre(genre);
    }

    public void DeleteGenre(Genre genre)
    {
        genresRepository.DeleteGenre(genre);
    }
}