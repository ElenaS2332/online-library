using Online_Library.Domain.Entities;

namespace Online_Library.Repository.Interfaces;

public interface IUsersRepository
{
    IEnumerable<User> GetAllUsers();
    User? GetUser(string id);
    void InsertUser(User user);
    void UpdateUser(User user);
    void DeleteUser(User user);

    void SaveChanges();
}
