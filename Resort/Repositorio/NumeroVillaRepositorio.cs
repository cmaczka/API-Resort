using Resort.Datos;
using Resort.Modelos;
using Resort.Repositorio.IRepositorio;
using System.Linq.Expressions;

namespace Resort.Repositorio
{
    public class NumeroVillaRepositorio : Repositorio<NumeroVilla>, INumeroVillaRepositorio
    {
        private readonly ApplicationDBContext _db;
        public NumeroVillaRepositorio(ApplicationDBContext db):base(db)
        {
            _db = db;
        }

        public async Task<NumeroVilla> Actualizar(NumeroVilla entidad)
        {
            entidad.FechaModificacion = DateTime.Now;
            _db.NumeroVillas.Update(entidad);
            await _db.SaveChangesAsync();
            return entidad;
        }

        public void SetOriginalRowVersion(NumeroVilla villa, byte[] rowVersion)
        {
            _db.Entry(villa).Property(x => x.RowVersion).OriginalValue = rowVersion;
        }

    }
}
