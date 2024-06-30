using TrashCollectionAPI.Data.Repository;
using TrashCollectionAPI.Models;

namespace TrashCollectionAPI.Services
{
    public class StatusService : IStatusService
    {
        private readonly IStatusRepository _repository;
        public StatusService(IStatusRepository repository)
        {
            _repository = repository;
        }
        public void AddNewStatus(StatusModel status) => _repository.AddNewStatus(status);

        public void DeleteStatus(int id)
        {
            var Status = this.GetStatusById(id);
            if (Status != null)
            {
                _repository.DeleteStatus(Status);
            }
        }

        public IEnumerable<StatusModel> GetAllStatus() => _repository.GetAllStatus();

        public StatusModel GetStatusById(int id) => _repository.GetStatusById(id);
    }
}
