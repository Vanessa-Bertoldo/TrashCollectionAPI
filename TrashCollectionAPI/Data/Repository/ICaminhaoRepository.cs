using TrashCollectionAPI.Models;

namespace TrashCollectionAPI.Data.Repository
{
    public interface ICaminhaoRepository
    {
        IEnumerable<CaminhaoModel> GetAllCaminhoes();
        CaminhaoModel GetCaminhaoById(int id);
        void AddNewCaminhao(CaminhaoModel caminhao);
        void UpdateCaminhao(CaminhaoModel caminhao);
        void DeleteCaminhao(CaminhaoModel caminhao);
    }
}
