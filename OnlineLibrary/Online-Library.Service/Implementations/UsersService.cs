using Online_Library.Domain.Entities;
using Online_Library.Repository.Interfaces;
using Online_Library.Service.Interfaces;

namespace Online_Library.Service.Implementations;

public class UsersService(IUsersRepository usersRepository) : IUsersService
{
    public IEnumerable<User> GetAllUsers()
    {
        return usersRepository.GetAllUsers();
    }

    public User GetUser(string id)
    {
        return usersRepository.GetUser(id);
    }

    public void InsertUser(User user)
    {
        usersRepository.InsertUser(user);
    }

    public void UpdateUser(User user)
    {
        usersRepository.UpdateUser(user);
    }

    public void DeleteUser(User user)
    {
        usersRepository.DeleteUser(user);
    }

    public void SaveChanges()
    {
        usersRepository.SaveChanges();
    }
}