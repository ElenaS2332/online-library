using Online_Library.Domain.Dtos;
using Online_Library.Domain.Entities;
using Online_Library.Domain.Exceptions;
using Online_Library.Repository.Implementations;
using Online_Library.Repository.Interfaces;
using Online_Library.Service.Interfaces;

namespace Online_Library.Service.Implementations;

public class BooksService(
    IBooksRepository booksRepository,
    IGenresRepository genresRepository,
    IAuthorsRepository authorsRepository) : IBooksService
{
    public Book GetBook(Guid id)
    {
        return booksRepository.GetBook(id);
    }

    public async Task<IEnumerable<Book>> GetAllBooksAsync()
    {
        return await booksRepository.GetAllBooksAsync();
    }

    public async Task<Book?> GetBookAsync(Guid id)
    {
        return await booksRepository.GetBookAsync(id);
    }

    public async Task<Book> InsertBookAsync(BookDto bookDto)
    {
        var author = await authorsRepository.GetAuthorAsync(bookDto.AuthorId);

        if (author is null)
        {
            throw new AuthorNotFoundException();
        }
        
        var genre = await genresRepository.GetGenreAsync(bookDto.GenreId);

        if (genre is null)
        {
            throw new GenreNotFoundException();
        }

        var book = new Book
        {
            Id = Guid.NewGuid(),
            Title = bookDto.Title,
            Description = bookDto.Description,
            ISBN = bookDto.ISBN,
            PublishDate = bookDto.PublishDate,
            Author = author,
            Genre = genre
        };
        
        return await booksRepository.InsertBookAsync(book);
    }

    public async Task<Book> UpdateBookAsync(EditBookDto bookDto)
    {
        var book = await booksRepository.GetBookAsync(bookDto.Id);
        if (book is null)
        {
            throw new BookNotFoundException();
        }
        
        var author = await authorsRepository.GetAuthorAsync(bookDto.AuthorId!.Value);

        if (author is null)
        {
            throw new AuthorNotFoundException();
        }
        
        var genre = await genresRepository.GetGenreAsync(bookDto.GenreId!.Value);

        if (genre is null)
        {
            throw new GenreNotFoundException();
        }

        book.Title = bookDto.Title;
        book.Description = bookDto.Description;
        book.ISBN = bookDto.ISBN;
        book.PublishDate = bookDto.PublishDate;
        book.Author = author;
        book.Genre = genre;
        
        return await booksRepository.UpdateBookAsync(book);
    }

    public async Task DeleteBookAsync(Book book)
    {
        await booksRepository.DeleteBookAsync(book);
    }
}