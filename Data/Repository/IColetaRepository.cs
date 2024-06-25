using TrashCollectionAPI.Models;

namespace TrashCollectionAPI.Data.Repository
{
    public interface IColetaRepository
    {
        IEnumerable<ColetaModel> GetAllColetas();
        ColetaModel GetColetaById(int id);
        void UpdateColeta(ColetaModel coleta);    
        void DeleteColeta(ColetaModel coleta);
        void AddNewColeta(ColetaModel coleta);


    }
}
