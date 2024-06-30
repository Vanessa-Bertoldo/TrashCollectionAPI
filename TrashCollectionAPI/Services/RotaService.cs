using TrashCollectionAPI.Data.Repository;
using TrashCollectionAPI.Models;

namespace TrashCollectionAPI.Services
{
    public class RotaService : IRotaService
    {
        private readonly IRotaRepository _repository;
        public RotaService(IRotaRepository repository)
        {
            _repository = repository;
        }
        public void AddNewRota(RotaModel rota) => _repository.AddNewRota(rota);

        public void DeleteRota(int id)
        {

            var Status = this.GetRotaById(id);
            if (Status != null)
            {
                _repository.DeleteRota(Status);
            }
        }

        public IEnumerable<RotaModel> GetAllRotas() => _repository.GetAllRotas();

        public IEnumerable<RotaModel> GetAllRotas(int idColeta) => _repository.GetAllRotas(idColeta);

        public RotaModel GetRotaById(int id) => _repository.GetRotaById(id);

        public void UpdateRota(RotaModel rota)
        {
            throw new NotImplementedException();
        }
    }
}
