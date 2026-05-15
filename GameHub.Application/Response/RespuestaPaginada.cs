namespace GameHub.Application.Response
{
    public class RespuestaPaginada<T>
    {
        public IEnumerable<T> Elementos { get; set; } = Enumerable.Empty<T>(); // Lista vacía por defecto

        public int TotalPaginas { get; set; }       // Número total de páginas

        public int NumeroPagina { get; set; }       // Número de página actual

        public int TotalElementos { get; set; }     

        public int TamanoPagina { get; set; }       // Registros por página

       
        public RespuestaPaginada(IEnumerable<T> elementos, int totalElementos, int numeroPagina, int tamanoPagina)
        {
            Elementos = elementos;
            TotalElementos = totalElementos;
            NumeroPagina = numeroPagina < 1 ? 1 : numeroPagina;
            TamanoPagina = tamanoPagina < 1 ? 10 : tamanoPagina;
            TotalPaginas = (int)Math.Ceiling(totalElementos / (double)TamanoPagina);
        }
    }
}