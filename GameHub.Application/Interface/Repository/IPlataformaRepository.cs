using GameHub.Domain.Entities;

namespace GameHub.Application.Interface.Repository
{
    public interface IPlataformaRepository
    {
        Task<IEnumerable<Plataforma>> ObtenerTodosAsync();

        Task<Plataforma?> ObtenerPorIdAsync(int id);

        Task CrearAsync(Plataforma plataforma);

        Task ActualizarAsync(Plataforma plataforma);

        Task EliminarAsync(Plataforma plataforma);
    }
}