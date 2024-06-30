using System.Net.NetworkInformation;
using TrashCollectionAPI.Data.Contexts;
using TrashCollectionAPI.Models;

namespace TrashCollectionAPI.Data.Repository
{
    public class StatusRepository : IStatusRepository
    {
        private readonly DatabaseContext _context;
        public StatusRepository(DatabaseContext context)
        {
            _context = context;
        }
        public void AddNewStatus(StatusModel status) 
        { 
            _context.Add(status); 
            _context.SaveChanges(); 
        }

        public void DeleteStatus(StatusModel status)
        {
            _context.Remove(status);
            _context.SaveChanges();
        }

        public IEnumerable<StatusModel> GetAllStatus() => _context.Status.ToList();

        public StatusModel GetStatusById(int id) => _context.Status.Find(id);

    }
}
