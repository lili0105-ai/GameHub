using GameHub.Domain.Entities;

namespace GameHub.Application.Interface.Repository
{
    public interface IRefreshTokenRepository
    {
        Task GuardarAsync(RefreshToken token);

        Task<RefreshToken?> ObtenerAsync(string token);

        Task ActualizarAsync(RefreshToken token);
    }
}