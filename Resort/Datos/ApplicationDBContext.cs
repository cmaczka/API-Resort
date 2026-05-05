using Microsoft.EntityFrameworkCore;
using Resort.Modelos;

namespace Resort.Datos
{
    public class ApplicationDBContext: DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
        }
        public DbSet<Modelos.Villa> Villas { get; set; }
        public DbSet<Modelos.NumeroVilla> NumeroVillas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Villa>()
                   .Property(v => v.RowVersion)
                   .IsRowVersion(); // ✅ marca rowversion/concurrency token

            modelBuilder.Entity<Modelos.Villa>().HasData(
                new Modelos.Villa
                {
                    Id = 1,
                    Nombre = "Villa 1",
                    Detalle = "Detalle de la Villa 1",
                    Tarifa = 100.0,
                    Ocupantes = 3,
                    MetrosCuadrados = 50.0,
                    ImageUrl = "https://example.com/villa1.jpg",
                    Amenidad = "Amenidad de la Villa 1",
                    FechaCreacion = DateTime.Now,
                    FechaActualizacion = DateTime.Now
                },
                new Modelos.Villa
                {
                    Id = 2,
                    Nombre = "Villa 2",
                    Detalle = "Detalle de la Villa 2",
                    Tarifa = 150.0,
                    Ocupantes = 4,
                    MetrosCuadrados = 60.0,
                    ImageUrl = "https://example.com/villa2.jpg",
                    Amenidad = "Amenidad de la Villa 2",
                    FechaCreacion = DateTime.Now,
                    FechaActualizacion = DateTime.Now
                },
                new Modelos.Villa
                {
                    Id = 3,
                    Nombre = "Villa 3",
                    Detalle = "Detalle de la Villa 3",
                    Tarifa = 200.0,
                    Ocupantes = 5,
                    MetrosCuadrados = 70.0,
                    ImageUrl = "https://example.com/villa3.jpg",
                    Amenidad = "Amenidad de la Villa 3",
                    FechaCreacion = DateTime.Now,
                    FechaActualizacion = DateTime.Now
                }
            );
        }
    }
}
