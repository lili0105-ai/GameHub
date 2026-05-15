using System.ComponentModel.DataAnnotations;

namespace GameHub.Application.Dtos.Usuario
{
    public class UsuarioLoginDto
    {
        [Required(ErrorMessage = "El email del usuario es requerido.")]
        [EmailAddress(ErrorMessage = "El email no es válido.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "La contraseña del usuario es requerida.")]
        public string Password { get; set; } = string.Empty;
    }
}