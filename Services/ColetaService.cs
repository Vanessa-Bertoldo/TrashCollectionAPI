using TrashCollectionAPI.Data.Repository;
using TrashCollectionAPI.Models;

namespace TrashCollectionAPI.Services
{
    public class ColetaService : IColetaService
    {
        private readonly IColetaRepository _repository;
        public ColetaService(IColetaRepository repository)
        {
            _repository = repository;
        }
        public void AddNewColeta(ColetaModel coleta) => _repository.AddNewColeta(coleta);
        public void DeleteColeta(int id)
        {
           var coleta = _repository.GetColetaById(id);
           if(coleta != null)
            {
                _repository.DeleteColeta(coleta);
            }
        }

        public IEnumerable<ColetaModel> GetAllColetas() => _repository.GetAllColetas();


        public ColetaModel GetColetaById(int id) => _repository.GetColetaById(id);

        public void UpdateColeta(ColetaModel coleta) => _repository.UpdateColeta(coleta);
    }
}
