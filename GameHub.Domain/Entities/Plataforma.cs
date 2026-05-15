namespace GameHub.Domain.Entities
{
    public class Plataforma
    {
        public int Id { get; set; }
        public string NombrePlataforma { get; set; } = null!;
        public string Fabricante { get; set; } = null!;
        public DateOnly FechaLanzamiento { get; set; }
        public DateOnly FechaRegistro { get; private set; } = DateOnly.FromDateTime(DateTime.Now);
        public virtual ICollection<VideojuegoPlataforma> VideojuegoPlataformas { get; set; } = new List<VideojuegoPlataforma>();
    }
}