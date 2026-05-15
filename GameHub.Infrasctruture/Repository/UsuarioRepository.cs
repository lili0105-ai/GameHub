using Ecommerce.Application.Interface.Repository;
using GameHub.Application.Interface.Repository;
using GameHub.Domain.Entities;
using GameHub.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GameHub.Infrastructure.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly ApplicationDbContext _context;

        public UsuarioRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> ContarAsync()
        {
            return await _context.Users.CountAsync();
        }

        public async Task<ApplicationUser?> ObtenerPorIdAsync(string id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<IEnumerable<ApplicationUser>> ObtenerTodosAsync(int pagina, int tamano)
        {
            return await _context.Users
                .AsNoTracking()
                .OrderBy(u => u.NombreDesarrollador)
                .Skip((pagina - 1) * tamano)
                .Take(tamano)
                .ToListAsync();
        }
    }
}