using Online_Library.Domain.Entities;

namespace Online_Library.Repository.Interfaces;

public interface IAuthorsRepository
{
    Task<IEnumerable<Author>> GetAllAuthors();
    Task<Author> GetAuthor(Guid id);
    Task InsertAuthor(Author author);
    Task UpdateAuthor(Author author);
    Task DeleteAuthor(Author author);
}