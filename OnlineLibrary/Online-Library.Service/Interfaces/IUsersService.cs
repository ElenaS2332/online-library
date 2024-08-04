using Online_Library.Domain.Entities;

namespace Online_Library.Service.Interfaces;

public interface IUsersService
{
    IEnumerable<User> GetAllUsers();
    User? GetUser(Guid id);
    void InsertUser(User user);
    void UpdateUser(User user);
    void DeleteUser(User user);
}