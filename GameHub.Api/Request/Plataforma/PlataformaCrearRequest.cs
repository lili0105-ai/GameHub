using System.ComponentModel.DataAnnotations;

namespace GameHub.Api.Request.Plataforma
{
    public class PlataformaCrearRequest
    {
        [Required(ErrorMessage = "El nombre de la plataforma es requerido.")]
        [MaxLength(50, ErrorMessage = "El nombre no debe exceder los 50 caracteres.")]
        public string NombrePlataforma { get; set; } = null!;

        [Required(ErrorMessage = "El fabricante es requerido.")]
        [MaxLength(100, ErrorMessage = "El fabricante no debe exceder los 100 caracteres.")]
        public string Fabricante { get; set; } = null!;

        [Required(ErrorMessage = "La fecha de lanzamiento es requerida.")]
        public DateOnly FechaLanzamiento { get; set; }
    }
}