using GameHub.Application.Dtos.Usuario;

namespace GameHub.Application.Response
{
    public class RespuestaLoginDto
    {
        public UsuarioDto Usuario { get; set; } = null!;
        public string AccessToken { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
        public DateTime ExpiraEn { get; set; }
    }
}