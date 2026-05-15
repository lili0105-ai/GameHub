namespace GameHub.Domain.Entities
{
    public class Genero
    {
        public int Id { get; set; }

        public string NombreGenero { get; set; } = null!;

        public string Descripcion { get; set; } = null!;

        public DateOnly FechaRegistro { get; private set; } = DateOnly.FromDateTime(DateTime.Now);

        public virtual ICollection<Videojuego> Videojuegos { get; set; } = new List<Videojuego>();
    }
}