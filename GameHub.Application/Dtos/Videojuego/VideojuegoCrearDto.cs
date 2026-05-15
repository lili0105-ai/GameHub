
namespace GameHub.Application.Dtos.Videojuego
{
    public class VideojuegoCrearDto
    {
        public string Titulo { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public DateOnly FechaLanzamiento { get; set; }
        public decimal Precio { get; set; }
        public string UrlImagen { get; set; } = null!;

        // FK
        public int GeneroId { get; set; }
        public string UserId { get; set; } = null!;
    }
}
