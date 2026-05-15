using GameHub.Application.Dtos.Usuario;

namespace GameHub.Application.Interface.Service
{
    public interface IUsuarioService
    {
        Task<UsuarioDto?> ObtenerPorIdAsync(string id);
        Task<IEnumerable<UsuarioDto>> ObtenerTodosAsync(int pagina, int tamano);
        Task<int> ContarAsync();
    }
}