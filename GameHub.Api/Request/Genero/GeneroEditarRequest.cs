using System.ComponentModel.DataAnnotations;

namespace GameHub.Api.Request.Genero
{
    public class GeneroEditarRequest
    {
        [Required(ErrorMessage = "El nombre del género es requerido.")]
        [MaxLength(50, ErrorMessage = "El nombre del género no debe exceder los 50 caracteres.")]
        public string NombreGenero { get; set; } = null!;

        [Required(ErrorMessage = "La descripción es requerida.")]
        [MaxLength(250, ErrorMessage = "La descripción no debe exceder los 250 caracteres.")]
        public string Descripcion { get; set; } = null!;
    }
}