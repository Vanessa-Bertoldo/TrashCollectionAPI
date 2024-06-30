using TrashCollectionAPI.Models;

namespace TrashCollectionAPI.Data.Repository
{
    public interface IRotaRepository
    {
        IEnumerable<RotaModel> GetAllRotas();
        IEnumerable<RotaModel> GetAllRotas(int idColeta);
        RotaModel GetRotaById(int id);
        void AddNewRota(RotaModel rota);
        void UpdateRota(RotaModel rota);
        void DeleteRota(RotaModel rota);
    }
}
