using Online_Library.Domain.Entities;
using Online_Library.Repository.Interfaces;

namespace Online_Library.Repository.Implementations;

public class GenresRepository(ApplicationDbContext context) : IGenresRepository
{
    public IEnumerable<Genre> GetAllGenres()
    {
        return context.Genres.ToList();
    }

    public Genre GetGenre(Guid id)
    {
        return context.Genres.FirstOrDefault(g => g.Id == id);
    }

    public void InsertGenre(Genre genre)
    {
        context.Genres.Add(genre);
        context.SaveChanges();
    }

    public void UpdateGenre(Genre genre)
    {
        context.Genres.Update(genre);
        context.SaveChanges();
    }

    public void DeleteGenre(Genre genre)
    {
        context.Genres.Remove(genre);
        context.SaveChanges();
    }
}
