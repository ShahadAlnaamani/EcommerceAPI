using EcommerceTask.DTOs;
using EcommerceTask.Models;

namespace EcommerceTask.Services
{
    public interface IUserService
    {
        public List<UserOutDTO> GetAllUsers();
        UserOutDTO GetMyDetails(int ID);
        UserOutDTO GetUserByID(int ID);
        int AddAdmin(UserInDTO admin);
        int AddUser(UserInDTO user);
        List<UserOutDTO> GetUserByName(string Name);
        User Login(string email, string password);
        string PassHasher(string password);
        UserOutDTO GetUserByPhoneNo(string phone);
    }
}