using Online_Library.Domain.Dtos;
using Online_Library.Domain.Entities;
using Online_Library.Domain.Exceptions;
using Online_Library.Repository.Interfaces;
using Online_Library.Service.Interfaces;

namespace Online_Library.Service.Implementations;

public class ReadingListService(
    IUsersRepository usersRepository,
    IBooksInReadingListRepository booksInReadingListRepository,
    IReadingListRepository readingListRepository,
    IBooksService booksService) : IReadingListService
{
    public bool AddToReadingListConfirmed(BooksInReadingList model, string userId)
    {

        var loggedInUser = usersRepository.GetUser(userId);

        var userReadingList = loggedInUser.ReadingList;

        userReadingList.UserId = loggedInUser.Id;
        userReadingList.BooksInReadingList ??= new List<BooksInReadingList>(); ;
        
        model.Book = booksService.GetBook(model.BookId);
        model.ReadingList = userReadingList;
        
        userReadingList.BooksInReadingList.Add(model);
        loggedInUser.ReadingList = userReadingList;
        
        usersRepository.UpdateUser(loggedInUser);
        var allReadingLists = readingListRepository.GetAllReadingLists();
        // readingListRepository.UpdateReadingList(userReadingList);
        return true;
    }
    
    
    public bool RemoveBookFromReadingList(string userId, Guid bookId)
    {
        var loggedInUser = usersRepository.GetUser(userId);

        var userReadingList = loggedInUser.ReadingList;
        
        if (userReadingList is null)
        {
            // throw new ReadingListNotFoundException();
            return false;
        }
        
        if (userReadingList.BooksInReadingList is null)
        {
            // throw new ReadingListNotFoundException();
            return false;
        }
        
        var book = userReadingList.BooksInReadingList.FirstOrDefault(x => x.BookId == bookId);

        if (book is null)
        {
            // throw new BookNotFoundException();
            return false;
        }
        
        userReadingList.BooksInReadingList.Remove(book);

        readingListRepository.UpdateReadingList(userReadingList);
        return true;
    }
    
    public ReadingListDto GetReadingListInfo(string userId)
    {
        var loggedInUser = usersRepository.GetUser(userId);

        var userReadingList = loggedInUser.ReadingList;
        
        if (userReadingList is null)
        {
            throw new ReadingListNotFoundException();
        }        
        
        var allBooks= userReadingList.BooksInReadingList?.ToList();
        var totalCount = 0;
        
        if (allBooks is not null)
        {
             totalCount= allBooks.Count;
        }

        ReadingListDto dto = new ReadingListDto
        {
            BooksInReadingList = allBooks,
            Count = totalCount
        };
        
        return dto;
    }


    
    
}