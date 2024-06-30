using TrashCollectionAPI.Models;

namespace TrashCollectionAPI.Data.Repository
{
    public interface IStatusRepository
    {
        IEnumerable<StatusModel> GetAllStatus();
        StatusModel GetStatusById(int id);
        void AddNewStatus(StatusModel status);
        void DeleteStatus(StatusModel status);
    }
}
