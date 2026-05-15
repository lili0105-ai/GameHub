
using GameHub.Domain.Entities;

namespace Ecommerce.Application.Interface.Repository
{
    public interface IUsuarioRepository
    {
        Task<ApplicationUser?> ObtenerPorIdAsync(string id);
        Task<IEnumerable<ApplicationUser>> ObtenerTodosAsync(int pagina, int tamano);
        Task<int> ContarAsync();
    }
}