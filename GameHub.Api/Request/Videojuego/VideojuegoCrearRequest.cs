using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http; // <--- AGREGA ESTE USING

namespace GameHub.Api.Request.Videojuego
{
    public class VideojuegoCrearRequest
    {
        [Required(ErrorMessage = "El título del videojuego es requerido.")]
        [MaxLength(100, ErrorMessage = "El título del videojuego no debe exceder los 100 caracteres.")]
        public string Titulo { get; set; } = null!;

        [Required(ErrorMessage = "La descripción del videojuego es requerida.")]
        [MaxLength(250, ErrorMessage = "La descripción del videojuego no debe exceder los 250 caracteres.")]
        public string Descripcion { get; set; } = null!;

        [Required(ErrorMessage = "La fecha de lanzamiento es requerida.")]
        public DateOnly FechaLanzamiento { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "El precio del videojuego debe ser mayor a cero.")]
        public decimal Precio { get; set; }

        [Required(ErrorMessage = "La imagen del videojuego es requerida.")]
        public IFormFile UrlImagen { get; set; } = null!;

        [Required(ErrorMessage = "El género del videojuego es requerido.")]
        public int GeneroId { get; set; }

        [Required(ErrorMessage = "El usuario desarrollador es requerido.")]
        public string UserId { get; set; } = null!;
    }
}
