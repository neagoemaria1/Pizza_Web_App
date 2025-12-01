using Pizzeria_Toscana.Models;

namespace Pizzeria_Toscana.Services.Interfaces
{
    public interface IUserService
    {
        User GetCurrentUser();
        List<User> GetAllUsers();
        User GetUserById(int idUser);

        void AddUser(User user);
        void UpdateUser(User user);
        void DeleteUser(int idUser);
    }
}
 