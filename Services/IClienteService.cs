using TrashCollectionAPI.Models;

namespace TrashCollectionAPI.Services
{
    public interface IColetaService
    {
        IEnumerable<ColetaModel> GetAllColetas();
        ColetaModel GetColetaById(int id);
        void UpdateColeta(ColetaModel coleta);
        void DeleteColeta(int id);
        void AddNewColeta(ColetaModel coleta);
    }
}
