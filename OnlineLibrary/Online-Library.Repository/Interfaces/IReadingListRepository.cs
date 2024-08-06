using Online_Library.Domain.Entities;

namespace Online_Library.Repository.Interfaces;

public interface IReadingListRepository
{
    IEnumerable<ReadingList> GetAllReadingLists();
    ReadingList? GetReadingList(Guid id);
    void UpdateReadingList(ReadingList readingList);
}