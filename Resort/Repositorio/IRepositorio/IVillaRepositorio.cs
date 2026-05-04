using Resort.Modelos;

namespace Resort.Repositorio.IRepositorio
{
    public interface IVillaRepositorio: IRepositorio<Villa>
    {
        Task<Villa> Actualizar(Villa entidad);
        void SetOriginalRowVersion(Villa villa, byte[] rowVersion);
    }
}
