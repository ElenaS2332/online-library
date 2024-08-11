using Online_Library.Domain.Entities;
using Online_Library.Repository.Interfaces;

namespace Online_Library.Repository.Implementations;

public class BooksInReadingListRepository(ApplicationDbContext context) : IBooksInReadingListRepository
{
    public IEnumerable<BooksInReadingList> GetAllBooksInReadingList()
    {
        return context.BooksInReadingLists.ToList();
    }

    public BooksInReadingList? GetBooksInReadingList(Guid id)
    {
        return context.BooksInReadingLists.FirstOrDefault(b => b.Id == id);
    }

    public void InsertBooksInReadingList(BooksInReadingList booksInReadingList)
    {
        context.BooksInReadingLists.Add(booksInReadingList);
        context.SaveChanges();
    }

    public void UpdateBooksInReadingList(BooksInReadingList booksInReadingList)
    {
        context.BooksInReadingLists.Update(booksInReadingList);
        context.SaveChanges();
    }

    public void DeleteBooksInReadingList(BooksInReadingList booksInReadingList)
    {
        context.BooksInReadingLists.Remove(booksInReadingList);
        context.SaveChanges();
    }

    public IEnumerable<BooksInReadingList> GetAllBooksInReadingListByReadingList(Guid readingListId)
    {
        return context.BooksInReadingLists
            .FirstOrDefault(b => b.ReadingListId == readingListId)
            .ReadingList.BooksInReadingList;
    }
}