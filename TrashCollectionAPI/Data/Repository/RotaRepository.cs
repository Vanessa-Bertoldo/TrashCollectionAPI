using TrashCollectionAPI.Data.Contexts;
using TrashCollectionAPI.Models;

namespace TrashCollectionAPI.Data.Repository
{
    public class RotaRepository : IRotaRepository
    {
        private readonly DatabaseContext _context;

        public RotaRepository(DatabaseContext context)
        {
            _context = context;
        }
        public void AddNewRota(RotaModel rota)
        {
            _context.Add(rota);
            _context.SaveChanges();
        }

        public void DeleteRota(RotaModel rota)
        {
            _context.Remove(rota);
            _context.SaveChanges();
        }

        public IEnumerable<RotaModel> GetAllRotas() => _context.Rota.ToList();

        public IEnumerable<RotaModel> GetAllRotas(int idColeta)
        {
            return _context.Rota
               .Where(r => r.IdColeta == idColeta)
               .ToList();
        }

        public RotaModel GetRotaById(int id) => _context.Rota.Find(id);

        public void UpdateRota(RotaModel rota)
        {
            _context.Update(rota);
            _context.SaveChanges();
        }
    }
}
