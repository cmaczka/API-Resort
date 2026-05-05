using Resort.Modelos;

namespace Resort.Repositorio.IRepositorio
{
    public interface INumeroVillaRepositorio: IRepositorio<NumeroVilla>
    {
        Task<NumeroVilla> Actualizar(NumeroVilla entidad);
        void SetOriginalRowVersion(NumeroVilla numeroVilla, byte[] rowVersion);
    }
}
