using TrashCollectionAPI.Models;

namespace TrashCollectionAPI.Services
{
    public interface ICaminhaoService
    {
        IEnumerable<CaminhaoModel> GetAllCaminhoes();
        CaminhaoModel GetCaminhaoById(int id);
        void AddNewCaminhao(CaminhaoModel caminhao);
        void UpdateCaminhao(CaminhaoModel caminhao);
        void DeleteCaminhao(int id);
    }
}
