﻿using Microsoft.EntityFrameworkCore;
using TrashCollectionAPI.Data.Contexts;
using TrashCollectionAPI.Models;

namespace TrashCollectionAPI.Data.Repository
{
    public class ColetaRepository : IColetaRepository
    {
        private readonly DatabaseContext _context;

        public ColetaRepository(DatabaseContext context)
        {
            _context = context;
        }
        public void AddNewColeta(ColetaModel coleta)
        {
            _context.Coleta.Add(coleta);
            _context.SaveChanges();
        }

        public void DeleteColeta(ColetaModel coleta)
        {
            _context.Coleta.Remove(coleta);
            _context.SaveChanges();
        }

        public IEnumerable<ColetaModel> GetAllColetas() => _context.Coleta.Include(x => x.Rotas).ToList();

        public ColetaModel GetColetaById(int id) => _context.Coleta
                .Include(x => x.Rotas)
                .FirstOrDefault(x => x.IdColeta == id);

        public void UpdateColeta(ColetaModel coleta)
        {
            _context.Update(coleta);
            _context.SaveChanges();
        }
    }
}
