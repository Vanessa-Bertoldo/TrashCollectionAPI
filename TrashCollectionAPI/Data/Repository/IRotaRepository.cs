using TrashCollectionAPI.Models;

namespace TrashCollectionAPI.Data.Repository
{
    public interface IRotaRepository
    {
        IEnumerable<RotaModel> GetAllRotas();
        RotaModel GetRotaById(int id);
        void AddNewRota(RotaModel rota);
        void UpdateRota(RotaModel rota);
        void DeleteRota(RotaModel rota);
    }
}
