using GameHub.Application.Dtos.Usuario;
using GameHub.Application.Response;

namespace GameHub.Application.Interface.Service
{
    public interface IAuthService
    {
        Task<RespuestaLoginDto> LoginAsync(UsuarioLoginDto dto);

        Task<UsuarioDto> RegistrarUsuarioAsync(UsuarioRegistroDto dto);

        Task<RespuestaLoginDto> RefreshTokenAsync(string refreshToken);
    }
}