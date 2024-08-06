using Online_Library.Domain.Entities;
using Online_Library.Domain.Exceptions;
using Online_Library.Repository.Interfaces;

namespace Online_Library.Service.Interfaces;

public class ReadingListService(
    IUsersRepository usersRepository,
    IBooksInReadingListRepository booksInReadingListRepository,
    IReadingListRepository readingListRepository)
{
    public bool AddToReadingListConfirmed(BooksInReadingList model, string userId)
    {

        var loggedInUser = usersRepository.GetUser(userId);

        var userReadingList = loggedInUser.ReadingList;

        if (userReadingList is null)
        {
            // throw new ReadingListNotFoundException();
            return false;
        }
        
        if (userReadingList.BooksInReadingList is null)
            userReadingList.BooksInReadingList = new List<BooksInReadingList>(); ;

        userReadingList.BooksInReadingList.Add(model);
        readingListRepository.UpdateReadingList(userReadingList);
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
    
    

    
    
}