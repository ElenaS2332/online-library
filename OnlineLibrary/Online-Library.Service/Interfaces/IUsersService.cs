using Online_Library.Domain.Entities;

namespace Online_Library.Service.Interfaces;

public interface IUsersService
{
    IEnumerable<User> GetAllUsers();
    User? GetUser(string id);
    void InsertUser(User user);
    void UpdateUser(User user);
    void DeleteUser(User user);

    void SaveChanges();
}