using TrashCollectionAPI.Data.Repository;
using TrashCollectionAPI.Models;

namespace TrashCollectionAPI.Services
{
    public class CaminhaoService : ICaminhaoService
    {
        private readonly ICaminhaoRepository _repository;
        public CaminhaoService(ICaminhaoRepository repository)
        {
            _repository = repository;
        }
        public void AddNewCaminhao(CaminhaoModel caminhao) => _repository.AddNewCaminhao(caminhao);

        public void DeleteCaminhao(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CaminhaoModel> GetAllCaminhoes() => _repository.GetAllCaminhoes();

        public CaminhaoModel GetCaminhaoById(int id) => _repository.GetCaminhaoById(id);

        public void UpdateCaminhao(CaminhaoModel caminhao)
        {
            throw new NotImplementedException();
        }
    }
}
