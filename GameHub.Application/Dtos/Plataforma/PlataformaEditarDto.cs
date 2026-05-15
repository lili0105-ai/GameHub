namespace GameHub.Application.Dtos.Plataforma
{
    public class PlataformaEditarDto
    {
        public string NombrePlataforma { get; set; } = null!;
        public string Fabricante { get; set; } = null!;
        public DateOnly FechaLanzamiento { get; set; }
    }
}