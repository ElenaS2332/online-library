using Online_Library.Domain.Entities;
using Online_Library.Repository.Interfaces;

namespace Online_Library.Repository.Implementations;

public class AuthorsRepository(ApplicationDbContext context) : IAuthorsRepository
{
    public IEnumerable<Author> GetAllAuthors()
    {
        return context.Authors.ToList();
    }

    public Author GetAuthor(Guid id)
    {
        return context.Authors.FirstOrDefault(a => a.Id == id);
    }

    public void InsertAuthor(Author author)
    {
        context.Authors.Add(author);
        context.SaveChanges();
    }

    public void UpdateAuthor(Author author)
    {
        context.Authors.Update(author);
        context.SaveChanges();
    }

    public void DeleteAuthor(Author author)
    {
        context.Authors.Remove(author);
        context.SaveChanges();    
    }
}
