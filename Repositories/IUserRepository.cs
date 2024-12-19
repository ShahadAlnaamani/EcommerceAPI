using EcommerceTask.Models;

namespace EcommerceTask.Repositories
{
    public interface IUserRepository
    {
        int AddUser(User user);
        User GetUserByEmail(string email, string password);
        User GetUserByID(int ID);
        List<User> GetUsersByName(string name);
        User GetUserByPhone(string phone);
        public List<User> GetAllUsers();
        string GetHashedPass(string email);
    }
}