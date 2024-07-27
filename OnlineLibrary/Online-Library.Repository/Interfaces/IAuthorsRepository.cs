using Online_Library.Domain.Entities;

namespace Online_Library.Repository.Interfaces;

public interface IAuthorsRepository
{
    IEnumerable<Author> GetAllAuthors();
    Author GetAuthor(Guid id);
    void InsertAuthor(Author author);
    void UpdateAuthor(Author author);
    void DeleteAuthor(Author author);
}