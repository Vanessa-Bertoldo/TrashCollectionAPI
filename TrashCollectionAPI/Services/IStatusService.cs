using TrashCollectionAPI.Models;

namespace TrashCollectionAPI.Services
{
    public interface IStatusService
    {
        IEnumerable<StatusModel> GetAllStatus();
        StatusModel GetStatusById(int id);
        void AddNewStatus(StatusModel status);
        void DeleteStatus(int id);
    }
}
