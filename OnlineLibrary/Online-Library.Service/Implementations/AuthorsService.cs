using Online_Library.Domain.Entities;
using Online_Library.Repository.Interfaces;
using Online_Library.Service.Interfaces;

namespace Online_Library.Service.Implementations;

public class AuthorsService(IAuthorsRepository authorsRepository) : IAuthorsService
{
    public async Task<IEnumerable<Author>> GetAllAuthors()
    {
        return await authorsRepository.GetAllAuthors();
    }

    public async Task<Author?> GetAuthor(Guid id)
    {
        return await authorsRepository.GetAuthor(id);
    }

    public async Task InsertAuthor(Author author)
    {
        await authorsRepository.InsertAuthor(author);
    }

    public async Task UpdateAuthor(Author author)
    {
        await authorsRepository.UpdateAuthor(author);
    }

    public async Task DeleteAuthor(Author author)
    {
        await authorsRepository.DeleteAuthor(author);
    }
}