using TrashCollectionAPI.Models;

namespace TrashCollectionAPI.Services
{
    public interface IStatusService
    {
        IEnumerable<StatusModel> GetAllStatuses();
        StatusModel GetStatusById(int id);
        void AddNewStatus(StatusModel status);
        void UpdateStatus(StatusModel status);
        void DeleteStatus(int id);
    }
}
