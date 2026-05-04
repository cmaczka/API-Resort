using Resort.Datos;
using Resort.Modelos;
using Resort.Repositorio.IRepositorio;
using System.Linq.Expressions;

namespace Resort.Repositorio
{
    public class VillaRepositorio : Repositorio<Villa>, IVillaRepositorio
    {
        private readonly ApplicationDBContext _db;
        public VillaRepositorio(ApplicationDBContext db):base(db)
        {
            _db = db;
        }

        public async Task<Villa> Actualizar(Villa entidad)
        {
            entidad.FechaActualizacion = DateTime.Now;
            _db.Villas.Update(entidad);
            await _db.SaveChangesAsync();
            return entidad;
        }

        public void SetOriginalRowVersion(Villa villa, byte[] rowVersion)
        {
            _db.Entry(villa).Property(x => x.RowVersion).OriginalValue = rowVersion;
        }

    }
}
