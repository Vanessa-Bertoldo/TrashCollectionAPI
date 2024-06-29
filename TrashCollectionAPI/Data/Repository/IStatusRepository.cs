using TrashCollectionAPI.Models;

namespace TrashCollectionAPI.Data.Repository
{
    public interface IStatusRepository
    {
        IEnumerable<StatusModel> GetAllStatuses();
        StatusModel GetStatusById(int id);
        void AddNewStatus(StatusModel status);
        void UpdateStatus(StatusModel status);
        void DeleteStatus(int id);
    }
}
