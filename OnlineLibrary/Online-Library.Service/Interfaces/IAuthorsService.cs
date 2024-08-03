using Online_Library.Domain.Entities;

namespace Online_Library.Service.Interfaces;

public interface IAuthorsService
{
    IEnumerable<Author> GetAllAuthors();
    Author? GetAuthor(Guid id);
    Task<IEnumerable<Author>> GetAllAuthorsAsync();
    Task<Author?> GetAuthorAsync(Guid id);
    Task InsertAuthorAsync(Author author);
    Task UpdateAuthorAsync(Author author);
    Task DeleteAuthorAsync(Author author);
}