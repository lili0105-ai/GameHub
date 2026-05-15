using GameHub.Application.Interface.Repository;
using GameHub.Domain.Entities;
using GameHub.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GameHub.Infrastructure.Repository
{
    public class VideojuegoRepository : IVideojuegoRepository
    {
        private readonly ApplicationDbContext _context;

        public VideojuegoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Videojuego?> ObtenerPorIdAsync(int id)
        {
            return await _context.Videojuegos
                .Include(v => v.Genero)
                .Include(v => v.User)
                .FirstOrDefaultAsync(v => v.Id == id);
        }

        public async Task<IEnumerable<Videojuego>> ObtenerPaginadoAsync(int pagina, int tamano)
        {
            return await _context.Videojuegos
                .Include(v => v.Genero)
                .Include(v => v.User)
                .Skip((pagina - 1) * tamano)
                .Take(tamano)
                .ToListAsync();
        }

        public async Task<IEnumerable<Videojuego>> BuscarPaginadoAsync(string valor, int pagina, int tamano)
        {
            return await _context.Videojuegos
                .Include(v => v.Genero)
                .Include(v => v.User)
                .Where(v => v.Titulo.Contains(valor) || v.Descripcion.Contains(valor))
                .Skip((pagina - 1) * tamano)
                .Take(tamano)
                .ToListAsync();
        }

        public async Task<int> ContarAsync()
        {
            return await _context.Videojuegos.CountAsync();
        }

        public async Task<int> ContarBusquedaAsync(string valor)
        {
            return await _context.Videojuegos
                .Where(v => v.Titulo.Contains(valor) || v.Descripcion.Contains(valor))
                .CountAsync();
        }

        public async Task<Videojuego> CrearAsync(Videojuego videojuego)
        {
            _context.Videojuegos.Add(videojuego);
            await _context.SaveChangesAsync();

            return videojuego;
        }

        public async Task ActualizarAsync(Videojuego videojuego)
        {
            _context.Videojuegos.Update(videojuego);
            await _context.SaveChangesAsync();
        }

        public async Task EliminarAsync(int id)
        {
            var videojuego = await _context.Videojuegos.FindAsync(id);

            if (videojuego != null)
            {
                _context.Videojuegos.Remove(videojuego);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Videojuego>> ObtenerPorUsuarioAsync(string userId)
        {
            return await _context.Videojuegos
                .Include(v => v.Genero)
                .Include(v => v.User)
                .Where(v => v.UserId == userId)
                .ToListAsync();
        }

        public async Task<int> ContarPorUsuarioAsync(string userId)
        {
            return await _context.Videojuegos
                .CountAsync(v => v.UserId == userId);
        }
    }
}