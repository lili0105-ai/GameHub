namespace GameHub.Domain.Entities
{
    public class Videojuego
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public DateOnly FechaLanzamiento { get; set; }
        public decimal Precio { get; set; }
        public string UrlImagen { get; set; } = null!;
        public DateOnly FechaRegistro { get; private set; } = DateOnly.FromDateTime(DateTime.Now);

        public string? UserId { get; set; }
        public ApplicationUser? User { get; set; }

        public int GeneroId { get; set; }
        public virtual Genero Genero { get; set; } = null!;

        public virtual ICollection<VideojuegoPlataforma> VideojuegoPlataformas { get; set; } = new List<VideojuegoPlataforma>();
    }
}