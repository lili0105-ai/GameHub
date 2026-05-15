using GameHub.Domain.Entities;

namespace GameHub.Application.Interface.Repository
{
    public interface IVideojuegoRepository
    {
        Task<Videojuego?> ObtenerPorIdAsync(int id);
        Task<IEnumerable<Videojuego>> ObtenerPaginadoAsync(int pagina, int tamano);
        Task<IEnumerable<Videojuego>> BuscarPaginadoAsync(string valor, int pagina, int tamano);
        Task<int> ContarAsync();
        Task<int> ContarBusquedaAsync(string valor);
        Task<Videojuego> CrearAsync(Videojuego videojuego);
        Task ActualizarAsync(Videojuego videojuego);
        Task EliminarAsync(int id);
        Task<IEnumerable<Videojuego>> ObtenerPorUsuarioAsync(string userId);
        Task<int> ContarPorUsuarioAsync(string userId);
    }
}