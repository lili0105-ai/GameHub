using GameHub.Application.Interface.Repository;
using GameHub.Domain.Entities;
using GameHub.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GameHub.Infrastructure.Repository
{
    public class PlataformaRepository : IPlataformaRepository
    {
        private readonly ApplicationDbContext _context;

        public PlataformaRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Plataforma>> ObtenerTodosAsync()
        {
            return await _context.Plataformas
                .ToListAsync();
        }

        public async Task<Plataforma?> ObtenerPorIdAsync(int id)
        {
            return await _context.Plataformas
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task CrearAsync(Plataforma plataforma)
        {
            _context.Plataformas.Add(plataforma);
            await _context.SaveChangesAsync();
        }

        public async Task ActualizarAsync(Plataforma plataforma)
        {
            _context.Plataformas.Update(plataforma);
            await _context.SaveChangesAsync();
        }

        public async Task EliminarAsync(Plataforma plataforma)
        {
            _context.Plataformas.Remove(plataforma);
            await _context.SaveChangesAsync();
        }
    }
}