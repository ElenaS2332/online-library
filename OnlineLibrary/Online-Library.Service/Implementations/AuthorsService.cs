using Online_Library.Domain.Entities;
using Online_Library.Repository.Interfaces;
using Online_Library.Service.Interfaces;

namespace Online_Library.Service.Implementations;

public class AuthorsService(IAuthorsRepository authorsRepository) : IAuthorsService
{
    public IEnumerable<Author> GetAllAuthors()
    {
        return authorsRepository.GetAllAuthors();
    }

    public Author GetAuthor(Guid id)
    {
        return authorsRepository.GetAuthor(id);
    }

    public void InsertAuthor(Author author)
    {
        authorsRepository.InsertAuthor(author);
    }

    public void UpdateAuthor(Author author)
    {
        authorsRepository.UpdateAuthor(author);
    }

    public void DeleteAuthor(Author author)
    {
        authorsRepository.DeleteAuthor(author);
    }
}