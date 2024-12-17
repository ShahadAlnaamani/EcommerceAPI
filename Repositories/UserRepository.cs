using EcommerceTask.Models;

namespace EcommerceTask.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        //Adds new users [returns User ID]
        public int AddUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
            return user.UID;
        }


        //Searches users by name [returns list of users/null]
        public List<User> GetUsersByName(string name)
        {
            return _context.Users.Where(u => u.Name == name).ToList();
        }


        //Searches users by ID [returns user/null]
        public User GetUserByID(int ID)
        {
            return _context.Users.Where(u => u.UID == ID).FirstOrDefault();
        }


        //Login function used to check if emails and passwords match [returns User]
        public User GetUserByEmail(string email, string password)
        {
            foreach (var user in _context.Users)
            {
                var pass = user.Password;
                var em = user.Email;
             }
            return _context.Users.Where(u => u.Email == email && u.Password == password).FirstOrDefault();
        }


        //Uses phone number to search for user [returns user]
        public User GetUserByPhone(string phone)
        {
            return _context.Users.Where(u => u.PhoneNumber == phone).FirstOrDefault();
        }


        //Gets all users [returns list of users]
        public List<User> GetAllUsers()
        {
            return _context.Users.ToList();
        }
    }
}
