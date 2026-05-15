using GameHub.Application.Dtos.Plataforma;

namespace GameHub.Application.Interface.Service
{
    public interface IPlataformaService
    {
        Task<IEnumerable<PlataformaDto>> ObtenerTodosAsync();

        Task<PlataformaDto?> ObtenerPorIdAsync(int id);

        Task<PlataformaDto> CrearAsync(PlataformaCrearDto dto);

        Task ActualizarAsync(int id, PlataformaEditarDto dto);

        Task EliminarAsync(int id);
    }
}