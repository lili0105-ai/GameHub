using GameHub.Application.Dtos.Videojuego;

namespace GameHub.Application.Interface.Service
{
    public interface IVideojuegoService
    {
        Task<VideojuegoDto?> ObtenerPorIdAsync(int id);
        Task<IEnumerable<VideojuegoDto>> ObtenerVideojuegoDtosAsync(int pagina, int tamano);
        Task<IEnumerable<VideojuegoDto>> BuscarVideojuegoDtosAsync(string valor, int pagina, int tamano);
        Task<int> ContarAsync();
        Task<int> ContarBusquedaAsync(string valor);

        Task<VideojuegoDto> CrearAsync(VideojuegoCrearDto dto, string imagenUrl);
        Task<VideojuegoDto> ActualizarAsync(int id, VideojuegoEditarDto dto, string? nuevaImagenUrl);
        Task EliminarAsync(int id);
    }
}