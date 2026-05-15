using GameHub.Application.Interface.Repository;
using GameHub.Domain.Entities;
using GameHub.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GameHub.Infrastructure.Repository
{
    public class GeneroRepository : IGeneroRepository
    {
        private readonly ApplicationDbContext _context;

        public GeneroRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Genero>> ObtenerTodosAsync()
        {
            return await _context.Generos
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Genero?> ObtenerPorIdAsync(int id)
        {
            return await _context.Generos.FindAsync(id);
        }

        public async Task<IEnumerable<Genero>> ObtenerPaginadoAsync(int pagina, int tamano)
        {
            return await _context.Generos
                .AsNoTracking()
                .OrderBy(g => g.Id)
                .Skip((pagina - 1) * tamano)
                .Take(tamano)
                .ToListAsync();
        }

        public async Task<IEnumerable<Genero>> BuscarPaginadoAsync(string valor, int pagina, int tamano)
        {
            return await _context.Generos
                .AsNoTracking()
                .Where(g => g.NombreGenero.ToLower().Contains(valor.ToLower()))
                .OrderBy(g => g.Id)
                .Skip((pagina - 1) * tamano)
                .Take(tamano)
                .ToListAsync();
        }

        public async Task<bool> ExisteNombreAsync(string nombre)
        {
            return await _context.Generos
                .AnyAsync(x => x.NombreGenero.ToLower() == nombre.ToLower());
        }

        public async Task<int> ContarAsync()
        {
            return await _context.Generos.CountAsync();
        }

        public async Task<int> ContarBusquedaAsync(string valor)
        {
            return await _context.Generos
                .Where(g => g.NombreGenero.ToLower().Contains(valor.ToLower()))
                .CountAsync();
        }

        public async Task CrearAsync(Genero entidad)
        {
            await _context.Generos.AddAsync(entidad);
            await _context.SaveChangesAsync();
        }

        public async Task ActualizarAsync(Genero entidad)
        {
            _context.Generos.Update(entidad);
            await _context.SaveChangesAsync();
        }

        public async Task EliminarAsync(Genero entidad)
        {
            _context.Generos.Remove(entidad);
            await _context.SaveChangesAsync();
        }
    }
}