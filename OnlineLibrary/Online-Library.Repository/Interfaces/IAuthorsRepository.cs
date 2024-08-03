using Online_Library.Domain.Entities;

namespace Online_Library.Repository.Interfaces;

public interface IAuthorsRepository
{

    IEnumerable<Author> GetAllAuthors();
    Author? GetAuthor(Guid id);
    Task<IEnumerable<Author>> GetAllAuthorsAsync();
    Task<Author?> GetAuthorAsync(Guid id);
    Task InsertAuthorAsync(Author author);
    Task UpdateAuthorAsync(Author author);
    Task DeleteAuthorAsync(Author author);
}