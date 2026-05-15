namespace GameHub.Application.Dtos.Usuario
{
    public class UsuarioDto
    {
        public string Id { get; set; } = null!;
        public string NombreDesarrollador { get; set; } = null!;
        public string? PhoneNumber { get; set; }
        public string Email { get; set; } = null!;
        public string Pais { get; set; } = null!;
        public string Rol { get; set; } = null!;
    }
}