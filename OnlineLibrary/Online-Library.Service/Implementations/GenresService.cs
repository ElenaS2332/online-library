using Online_Library.Domain.Entities;
using Online_Library.Domain.Exceptions;
using Online_Library.Repository.Interfaces;
using Online_Library.Service.Interfaces;

namespace Online_Library.Service.Implementations;

public class GenresService(IGenresRepository genresRepository) : IGenresService
{
    public async Task<IEnumerable<Genre>> GetAllGenres()
    {
        return await genresRepository.GetAllGenres();
    }

    public async Task<Genre?> GetGenre(Guid id)
    {
        return await genresRepository.GetGenre(id);
    }

    public async Task InsertGenre(Genre genre)
    {
        await genresRepository.InsertGenre(genre);
    }

    public async Task UpdateGenre(Genre genre)
    {
        var genreFromDatabase = await GetGenre(genre.Id);

        if (genreFromDatabase is null)
        {
            throw new GenreNotFoundException();
        }

        genreFromDatabase.Books = genre.Books;
        genreFromDatabase.Name = genre.Name;
        
        await genresRepository.UpdateGenre(genreFromDatabase);
    }

    public async Task DeleteGenre(Genre genre)
    {
        await genresRepository.DeleteGenre(genre);
    }
}