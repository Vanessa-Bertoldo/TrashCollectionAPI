using TrashCollectionAPI.Models;

namespace TrashCollectionAPI.Data.Repository
{
    public interface IUserRepository
    {
        void RegisterUser(UserModel user);
        UserModel Autenticacao(AuthModel auth);
    }
}
