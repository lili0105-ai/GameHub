using GameHub.Application.Dtos.Genero;

namespace GameHub.Application.Interface.Service
{
    public interface IGeneroService
    {
        Task<IEnumerable<GeneroDto>> ObtenerTodosAsync();

        Task<IEnumerable<GeneroDto>> ObtenerPaginadoAsync(int pagina, int tamano);

        Task<IEnumerable<GeneroDto>> BuscarPaginadoAsync(string valor, int pagina, int tamano);

        Task<int> ContarAsync();

        Task<int> ContarBusquedaAsync(string valor);

        Task<GeneroDto> CrearAsync(GeneroCrearDto dto);

        Task<GeneroDto?> ObtenerPorIdAsync(int id);

        Task ActualizarAsync(int id, GeneroEditarDto dto);

        Task EliminarAsync(int id);
    }
}