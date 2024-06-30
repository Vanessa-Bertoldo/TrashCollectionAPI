using System.Data;
using TrashCollectionAPI.Data.Contexts;
using TrashCollectionAPI.Models;

namespace TrashCollectionAPI.Data.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly DatabaseContext _context;

        public UserRepository(DatabaseContext context)
        {
            _context = context;
        }

        public UserModel Autenticacao(AuthModel auth)
        {
            var user = _context.User.SingleOrDefault(u => u.Username == auth.Username);
            if (user != null && BCrypt.Net.BCrypt.Verify(auth.Password, user.PasswordHash))
            {
                return new UserModel
                {
                    UserId = user.UserId,
                    Username = user.Username,
                    Role = user.Role
                };
            }
            return null;
        }

        public void RegisterUser(UserModel user)
        {
            _context.Add(user);
            _context.SaveChanges();
        }


    }
}
