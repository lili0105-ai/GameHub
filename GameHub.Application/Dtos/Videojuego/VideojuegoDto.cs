namespace GameHub.Application.Dtos.Videojuego
{
    public class VideojuegoDto
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public DateOnly FechaLanzamiento { get; set; }
        public decimal Precio { get; set; }
        public string UrlImagen { get; set; } = null!;

        // Datos del Género
        public int GeneroId { get; set; }
        public string NombreGenero { get; set; } = null!;

        // Datos del Desarrollador (Usuario de Identity)
        public string DesarrolladorId { get; set; } = null!;
        public string NombreDesarrollador { get; set; } = null!;
    }
}