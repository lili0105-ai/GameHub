using GameHub.Domain.Entities;

namespace GameHub.Application.Interface.Repository
{
    public interface IGeneroRepository
    {
        Task<IEnumerable<Genero>> ObtenerTodosAsync();
        Task<Genero?> ObtenerPorIdAsync(int id);

        Task<IEnumerable<Genero>> ObtenerPaginadoAsync(int pagina, int tamano);

        Task<IEnumerable<Genero>> BuscarPaginadoAsync(string valor, int pagina, int tamano);

        Task<bool> ExisteNombreAsync(string nombre);

        Task<int> ContarAsync();

        Task<int> ContarBusquedaAsync(string valor);

        Task CrearAsync(Genero genero);

        Task ActualizarAsync(Genero genero);

        Task EliminarAsync(Genero genero);
    }
}