using Microsoft.EntityFrameworkCore;

namespace Resort.Datos
{
    public class ApplicationDBContext: DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
        }
        public DbSet<Modelos.Villa> Villas { get; set; }
    }
}
