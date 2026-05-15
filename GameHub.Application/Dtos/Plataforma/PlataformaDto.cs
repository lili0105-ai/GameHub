namespace GameHub.Application.Dtos.Plataforma
{
    public class PlataformaDto
    {
        public int Id { get; set; }
        public string NombrePlataforma { get; set; } = null!;
        public string Fabricante { get; set; } = null!;
        public DateOnly FechaLanzamiento { get; set; }
    }
}