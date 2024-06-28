using TrashCollectionAPI.Models;

namespace TrashCollectionAPI.Services
{
    public interface IAuthService
    {
        UserModel Authenticate(string username, string password)
    }
}
