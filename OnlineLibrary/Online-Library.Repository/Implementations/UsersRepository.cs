using Online_Library.Domain.Entities;
using Online_Library.Repository.Interfaces;

namespace Online_Library.Repository.Implementations;

public class UsersRepository(ApplicationDbContext context) : IUsersRepository
{
    public IEnumerable<User> GetAllUsers()
    {
        return context.Users.ToList();
    }

    public User GetUser(Guid id)
    {
        // Improve logic for users
        return context.Users.FirstOrDefault(u => u.Id == id.ToString());
    }

    public void InsertUser(User user)
    {
        context.Users.Add(user);
        context.SaveChanges();
    }

    public void UpdateUser(User user)
    {
        context.Users.Update(user);
        context.SaveChanges();
    }

    public void DeleteUser(User user)
    {
        context.Users.Remove(user);
        context.SaveChanges();
    }
}
