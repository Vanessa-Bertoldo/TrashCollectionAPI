using TrashCollectionAPI.Models;

namespace TrashCollectionAPI.Services
{
    public interface IAuthService
    {
        void RegisterUser(UserModel user);
        TokenUserModel VerificaLogin(AuthModel auth);
    }
}
