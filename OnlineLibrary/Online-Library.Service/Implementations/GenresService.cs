using Online_Library.Domain.Entities;
using Online_Library.Domain.Exceptions;
using Online_Library.Repository.Interfaces;
using Online_Library.Service.Interfaces;

namespace Online_Library.Service.Implementations;

public class GenresService(IGenresRepository genresRepository) : IGenresService
{
    
    public IEnumerable<Genre> GetAllGenres()
    {
        return genresRepository.GetAllBGenres();
    }

    public Genre? GetGenre(Guid id)
    {
        return genresRepository.GetGenre(id);
    }

    public Genre? GetGenreByName(string name)
    {
        return genresRepository.GetGenreByName(name);
    }

    public async Task<IEnumerable<Genre>> GetAllGenresAsync()
    {
        return await genresRepository.GetAllGenresAsync();
    }

    public async Task<Genre?> GetGenreAsync(Guid id)
    {
        return await genresRepository.GetGenreAsync(id);
    }
    
    public async Task InsertGenreAsync(Genre genre)
    {
        await genresRepository.InsertGenreAsync(genre);
    }

    public async Task UpdateGenreAsync(Genre genre)
    {
        var genreFromDatabase = await GetGenreAsync(genre.Id);

        if (genreFromDatabase is null)
        {
            throw new GenreNotFoundException();
        }

        genreFromDatabase.Books = genre.Books;
        genreFromDatabase.Name = genre.Name;
        
        await genresRepository.UpdateGenreAsync(genreFromDatabase);
    }

    public async Task DeleteGenreAsync(Genre genre)
    {
        await genresRepository.DeleteGenreAsync(genre);
    }
}