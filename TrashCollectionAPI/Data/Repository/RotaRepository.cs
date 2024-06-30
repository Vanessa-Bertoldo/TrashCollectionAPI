﻿using TrashCollectionAPI.Data.Contexts;
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

        public RotaModel GetRotaById(int id) => _context.Rota.Find(id);

        public void UpdateRota(RotaModel rota)
        {
            throw new NotImplementedException();
        }
    }
}
