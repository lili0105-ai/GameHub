using System.ComponentModel.DataAnnotations;

namespace GameHub.Application.Dtos.Usuario
{
    public class UsuarioRegistroDto
    {
        [Required(ErrorMessage = "El nombre del desarrollador es requerido.")]
        public string NombreDesarrollador { get; set; } = null!;

        [Required(ErrorMessage = "El email del usuario es requerido.")]
        [EmailAddress(ErrorMessage = "El email no es válido.")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "La contraseña del usuario es requerida.")]
        public string Password { get; set; } = null!;

        [Required(ErrorMessage = "El rol del usuario es requerido.")]
        public string Rol { get; set; } = null!;

        [Required(ErrorMessage = "El país es requerido.")]
        public string Pais { get; set; } = null!;

        public DateOnly? FechaFundacion { get; set; }

        public string? PhoneNumber { get; set; }
    }
}