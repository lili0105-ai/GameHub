using GameHub.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GameHub.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Videojuego> Videojuegos { get; set; }
        public DbSet<Genero> Generos { get; set; }
        public DbSet<Plataforma> Plataformas { get; set; }
        public DbSet<VideojuegoPlataforma> VideojuegoPlataformas { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Videojuego>(static entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Titulo)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.FechaLanzamiento)
                    .IsRequired()
                    .HasColumnType("date");

                entity.Property(e => e.Precio)
                    .IsRequired()
                    .HasColumnType("decimal(10,2)");

                entity.Property(e => e.UrlImagen)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.GeneroId)
                    .IsRequired();

                // Ahora es string porque mapea al ID de IdentityUser
                entity.Property(e => e.UserId)
                    .IsRequired();

                entity.Property(e => e.FechaRegistro)
                    .IsRequired()
                    .HasColumnType("date");

                entity.HasIndex(e => e.Titulo).IsUnique();

                // Relacion con Genero
                entity.HasOne(v => v.Genero)
                    .WithMany(g => g.Videojuegos)
                    .HasForeignKey(v => v.GeneroId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .IsRequired();

                // Relación con Usuario (ApplicationUser actuando como Desarrollador)
                entity.HasOne(v => v.User)
                    .WithMany(u => u.Videojuegos)
                    .HasForeignKey(v => v.UserId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .IsRequired();

                entity.ToTable(table =>
                {
                    table.HasCheckConstraint("CK_Videojuego_Precio", "\"Precio\" > 0");
                });
            });

            builder.Entity<ApplicationUser>(static entity =>
            {
                entity.Property(e => e.NombreDesarrollador)
                    .IsRequired()
                    .HasMaxLength(75);

                // Opcional: Puedes mapear el resto de los campos si quieres ser explícita
                entity.Property(e => e.Pais)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.FechaFundacion)
                    .IsRequired()
                    .HasColumnType("date");

                entity.Property(e => e.FechaRegistro)
                    .IsRequired()
                    .HasColumnType("date");
            });

            builder.Entity<Genero>(static entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.NombreGenero)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.FechaRegistro)
                    .IsRequired()
                    .HasColumnType("date");

                entity.HasIndex(e => e.NombreGenero).IsUnique();
            });

            builder.Entity<Plataforma>(static entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.NombrePlataforma)
                    .IsRequired()
                    .HasMaxLength(80);

                entity.Property(e => e.Fabricante)
                    .IsRequired()
                    .HasMaxLength(80);

                entity.Property(e => e.FechaLanzamiento)
                    .IsRequired()
                    .HasColumnType("date");

                entity.Property(e => e.FechaRegistro)
                    .IsRequired()
                    .HasColumnType("date");

                entity.HasIndex(e => e.NombrePlataforma).IsUnique();
            });

            builder.Entity<VideojuegoPlataforma>(static entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.VideojuegoId)
                    .IsRequired();

                entity.Property(e => e.PlataformaId)
                    .IsRequired();

                entity.Property(e => e.FechaRegistro)
                    .IsRequired()
                    .HasColumnType("date");

                // Relacion con Videojuego 
                entity.HasOne(vp => vp.Videojuego)
                    .WithMany(v => v.VideojuegoPlataformas)
                    .HasForeignKey(vp => vp.VideojuegoId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .IsRequired();

                // Relacion con Plataforma
                entity.HasOne(vp => vp.Plataforma)
                    .WithMany(p => p.VideojuegoPlataformas)
                    .HasForeignKey(vp => vp.PlataformaId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .IsRequired();
            });
        }
    }
}