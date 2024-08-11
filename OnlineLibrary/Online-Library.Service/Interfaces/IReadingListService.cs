using Online_Library.Domain.Dtos;
using Online_Library.Domain.Entities;

namespace Online_Library.Service.Interfaces;

public interface IReadingListService
{
    bool AddToReadingListConfirmed(BooksInReadingList model, string userId);
    bool RemoveBookFromReadingList(string userId, Guid bookId);
    ReadingListDto GetReadingListInfo(string userId);

    void SaveChanges();
}