using Online_Library.Domain.Entities;
using Online_Library.Repository.Interfaces;

namespace Online_Library.Repository.Implementations;

public class ReadingListRepository(ApplicationDbContext context) : IReadingListRepository
{
    public IEnumerable<ReadingList> GetAllReadingLists()
    {
        return context.ReadingLists.ToList();
    }

    public ReadingList? GetReadingList(Guid id)
    {
        return context.ReadingLists.FirstOrDefault(r => r.Id == id);
    }

    public void UpdateReadingList(ReadingList readingList)
    {
        context.Update(readingList);
        context.SaveChanges();
    }
}