
using Microsoft.AspNetCore.Identity;

namespace GameHub.Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string NombreDesarrollador { get; set; } = null!;
        public  string Pais { get; set; } = null!;
        public DateOnly FechaFundacion { get; set; }
         public DateOnly FechaRegistro { get; private set; } = DateOnly.FromDateTime(DateTime.Now);

         //Navegación
         public virtual ICollection<Videojuego> Videojuegos { get; set; } = new List<Videojuego>();

        public virtual ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();
    }
}
