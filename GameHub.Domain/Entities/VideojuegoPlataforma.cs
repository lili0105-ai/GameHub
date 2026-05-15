namespace GameHub.Domain.Entities
{
    public class VideojuegoPlataforma
    {
        public int Id { get; set; }
        public int VideojuegoId { get; set; }
        public int PlataformaId { get; set; }
        public DateOnly FechaRegistro { get; private set; } = DateOnly.FromDateTime(DateTime.Now);
        public Videojuego Videojuego { get; set; } = null!;
        public Plataforma Plataforma { get; set; } = null!;


    }
}
