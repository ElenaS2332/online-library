using Online_Library.Domain.Entities;
using Online_Library.Domain.Exceptions;
using Online_Library.Repository.Interfaces;
using Online_Library.Service.Interfaces;

namespace Online_Library.Service.Implementations;

public class AuthorsService(IAuthorsRepository authorsRepository) : IAuthorsService
{
    public IEnumerable<Author> GetAllAuthors()
    {
        return authorsRepository.GetAllAuthors();
    }

    public Author? GetAuthor(Guid id)
    {
        return authorsRepository.GetAuthor(id);
    }

    public async Task<IEnumerable<Author>> GetAllAuthorsAsync()
    {
        return await authorsRepository.GetAllAuthorsAsync();
    }

    public async Task<Author?> GetAuthorAsync(Guid id)
    {
        return await authorsRepository.GetAuthorAsync(id);
    }

    public async Task InsertAuthorAsync(Author author)
    {
        await authorsRepository.InsertAuthorAsync(author);
    }

    public async Task UpdateAuthorAsync(Author author)
    {
        var authorFromDatabase = await GetAuthorAsync(author.Id);

        if (authorFromDatabase is null)
        {
            throw new AuthorNotFoundException();
        }

        authorFromDatabase.Books = author.Books;
        authorFromDatabase.Name = author.Name;
        authorFromDatabase.Surname = author.Surname;
        authorFromDatabase.DateOfBirth = author.DateOfBirth;
        
        await authorsRepository.UpdateAuthorAsync(authorFromDatabase);
    }

    public async Task DeleteAuthorAsync(Author author)
    {
        await authorsRepository.DeleteAuthorAsync(author);
    }
}