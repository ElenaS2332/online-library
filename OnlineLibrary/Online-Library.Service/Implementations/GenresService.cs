using Online_Library.Domain.Entities;
using Online_Library.Repository.Interfaces;
using Online_Library.Service.Interfaces;

namespace Online_Library.Service.Implementations;

public class GenresService(IGenresRepository genresRepository) : IGenresService
{
    public async Task<IEnumerable<Genre>> GetAllGenres()
    {
        return await genresRepository.GetAllGenres();
    }

    public async Task<Genre> GetGenre(Guid id)
    {
        return await genresRepository.GetGenre(id);
    }

    public async Task InsertGenre(Genre genre)
    {
        await genresRepository.InsertGenre(genre);
    }

    public async Task UpdateGenre(Genre genre)
    {
        await genresRepository.UpdateGenre(genre);
    }

    public async Task DeleteGenre(Genre genre)
    {
        await genresRepository.DeleteGenre(genre);
    }
}