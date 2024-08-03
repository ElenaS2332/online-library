using Online_Library.Domain.Entities;
using Online_Library.Domain.Exceptions;
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
        var authorFromDatabase = await GetAuthor(author.Id);

        if (authorFromDatabase is null)
        {
            throw new AuthorNotFoundException();
        }

        authorFromDatabase.Books = author.Books;
        authorFromDatabase.Name = author.Name;
        authorFromDatabase.Surname = author.Surname;
        authorFromDatabase.DateOfBirth = author.DateOfBirth;
        
        await authorsRepository.UpdateAuthor(authorFromDatabase);
    }

    public async Task DeleteAuthor(Author author)
    {
        await authorsRepository.DeleteAuthor(author);
    }
}