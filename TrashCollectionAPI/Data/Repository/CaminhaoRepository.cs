using Microsoft.EntityFrameworkCore;
using TrashCollectionAPI.Data.Contexts;
using TrashCollectionAPI.Models;

namespace TrashCollectionAPI.Data.Repository
{
    public class CaminhaoRepository : ICaminhaoRepository
    {
        private readonly DatabaseContext _context;
        public CaminhaoRepository(DatabaseContext context)
        {
            _context = context;
        }
        public void AddNewCaminhao(CaminhaoModel caminhao)
        {
            _context.Add(caminhao);
            _context.SaveChanges();
        }

        public void DeleteCaminhao(CaminhaoModel caminhao)
        {
            _context.Remove(caminhao);
            _context.SaveChanges();
        }

        public IEnumerable<CaminhaoModel> GetAllCaminhoes() => _context.Caminhao.Include(x => x.Status).ToList();

        public CaminhaoModel GetCaminhaoById(int id) => _context.Caminhao.Find(id);

        public void UpdateCaminhao(CaminhaoModel caminhao)
        {
            _context.Update(caminhao);
            _context.SaveChanges(true);
        }
    }
}
